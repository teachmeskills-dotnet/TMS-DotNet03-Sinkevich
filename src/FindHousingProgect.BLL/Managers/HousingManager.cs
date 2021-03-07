﻿using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.Common.Resources;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Managers
{
    public class HousingManager: IHousingManager
    {
        private readonly IRepository<Housing> _repositoryHousing;
        private readonly IUserManager _userManager;
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<Place> _repositoryPlace;
        public HousingManager(IRepository<Housing> repositoryHousing, IUserManager userManager, IRepository<User> repositoryUser, IRepository<Place> repositoryPlace)
        {
            _repositoryHousing = repositoryHousing ?? throw new ArgumentNullException(nameof(repositoryHousing));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
            _repositoryPlace = repositoryPlace ?? throw new ArgumentNullException(nameof(repositoryPlace));
        }
        public async Task CreateAsync(HousingDto housingDto)
        {
            housingDto = housingDto ?? throw new ArgumentNullException(nameof(housingDto));
            housingDto.Place.Type = "Housing type";

            await _repositoryPlace.CreateAsync(housingDto.Place);
           // await _repositoryHousing.SaveChangesAsync();

            var housing = new Housing
            {
              //  Id = housingDto.Id,
                Place = housingDto.Place,
                Name = housingDto.Name,
                UserId = housingDto.UserId,
                User = housingDto.User,
                BookedFrom = housingDto.BookedFrom,
                BookedTo = housingDto.BookedTo,
                PricePerDay = housingDto.PricePerDay,
                Scenery = housingDto.Scenery,
                Description = housingDto.Description,
                Address = housingDto.Address
               // State = orderDto.VendorId is null ? StateType.AwaitingVendor : StateType.AwaitingConfirm
            };
            await _repositoryHousing.CreateAsync(housing);
           // await _repositoryUser.GetEntityAsync();
            await _repositoryHousing.SaveChangesAsync();
        }
       public async Task<IEnumerable<Housing>> GetCurrentHousingsAsync(string email)
       {
            var userId = await _userManager.GetUserIdByEmailAsync(email);
            return await _repositoryHousing.GetListAsync(x=>x.UserId==userId);
       }
        public IEnumerable<Housing> GetAllHousings()
        {
            return  _repositoryHousing.GetAll();
        }
        public async Task DeleteAsync(string id, string userId)
        {
             var housing = await _repositoryHousing.GetEntityAsync(housing=>housing.Id==id && housing.UserId==userId);

            if (housing is null)
            {
                throw new KeyNotFoundException(ErrorResource.HousingNotFound);
            }

            _repositoryHousing.Delete(housing);
            await _repositoryHousing.SaveChangesAsync();
        }
        public async Task<HousingDto> GetHousingAsync(string id)
        {
            //var housing = await _repositoryHousing.GetEntityAsync(order => order.Id == id);
            var housing = await _repositoryHousing
            .GetAll().Include(x => x.Place).FirstOrDefaultAsync(order => order.Id == id);

            if (housing is null)
            {
                throw new KeyNotFoundException(ErrorResource.HousingNotFound);
            }

            //HousingDto housingDto = null;
            var housingsDto = new HousingDto
            {
                Id = housing.Id,
                UserId = housing.UserId,
                Place = housing.Place,
                PlaceId = housing.PlaceId,
                Address = housing.Address,
                Name = housing.Name,
                Description = housing.Description,
                Scenery=housing.Scenery,
                PricePerDay = housing.PricePerDay
            };
            return housingsDto;
        }
        public async Task UpdateHousingAsync(HousingDto housingDto, string userId)
        {
            housingDto = housingDto ?? throw new ArgumentNullException(nameof(housingDto));

            var housing = await _repositoryHousing
            .GetAll().Include(x => x.Place).FirstOrDefaultAsync(housings => housings.Id == housingDto.Id
               && housings.UserId == userId
             );

            // var housing = await _repositoryHousing.GetEntityAsync(housings => housings.Id == housingDto.Id
            //   && housings.UserId == userId
            // );

            if (housing is null)
            {
                throw new KeyNotFoundException(ErrorResource.HousingNotFound);
            }


            var result = ValidateToUpdate(housing, housingDto);
            if (result)
            {
                _repositoryPlace.Update(housing.Place);
                _repositoryHousing.Update(housing);
                await _repositoryHousing.SaveChangesAsync();
            }
        }
        static bool ValidateToUpdate(Housing housing, HousingDto housingDto)
        {
            bool updated = false;

            if (housing.Name != housingDto.Name)
            {
                housing.Name = housingDto.Name;
                updated = true;
            }
            if (housing.Place.Name != housingDto.Place.Name)
            {
                housing.Place.Name = housingDto.Place.Name;
                updated = true;
            }
            if (housing.Address != housingDto.Address)
            {
                housing.Address = housingDto.Address;
                updated = true;
            }
            if (housing.Scenery != housingDto.Scenery)
            {
                housing.Scenery = housingDto.Scenery;
                updated = true;
            }
            if (housing.Description != housingDto.Description)
            {
                housing.Description = housingDto.Description;
                updated = true;
            }

            if (housing.PricePerDay != housingDto.PricePerDay)
            {
                housing.PricePerDay = housingDto.PricePerDay;
                updated = true;
            }

            return updated;
        }
        public async Task<IEnumerable<Housing>> SearchHousingAsync(string userInput, DateTime? checkIn = null, DateTime? checkOut = null)
        {
            var housingDtos = new List<Housing>();

            var housings = await _repositoryHousing
                .GetAll().Include(x=>x.Place).Include(x=>x.Reservations)
                .AsNoTracking().ToListAsync();
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                housings = housings.Where(x => x.Name.Contains(userInput, StringComparison.OrdinalIgnoreCase) ||(x.Place!=null && x.Place.Name.Contains(userInput, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            if (checkIn!=null && checkOut!=null)
            {
                housings = housings.Where(x=> !x.Reservations.Any(r=>r.CheckIn >= checkIn.Value && r.CheckOut<= checkOut.Value)).ToList();
            }
            if (housings.Any())
            {
                foreach (var housing in housings)
                {
                    housingDtos.Add(new Housing
                    {
                        Id=housing.Id,
                        Name = housing.Name,
                        Address = housing.Address,
                        Place = housing.Place,
                        PricePerDay = housing.PricePerDay,
                        Scenery = housing.Scenery
                    });
                }
            }
            return housingDtos;
        }
    }
}
