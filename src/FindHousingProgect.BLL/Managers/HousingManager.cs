using FindHousingProject.BLL.Interfaces;
using FindHousingProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using FindHousingProject.BLL.Models;

namespace FindHousingProject.BLL.Managers
{
    public class HousingManager: IHousingManager
    {
        private readonly IRepository<Housing> _repositoryHousing;
        private readonly IUserManager _userManager;
        private readonly IRepository<User> _repositoryUser;
        public HousingManager(IRepository<Housing> repositoryHousing, IUserManager userManager, IRepository<User> repositoryUser)
        {
            _repositoryHousing = repositoryHousing ?? throw new ArgumentNullException(nameof(repositoryHousing));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
        }
        public async Task CreateAsync(HousingDto housingDto)
        {
            housingDto = housingDto ?? throw new ArgumentNullException(nameof(housingDto));

            var housing = new Housing
            {
                Id = housingDto.Id,
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
    }
}
