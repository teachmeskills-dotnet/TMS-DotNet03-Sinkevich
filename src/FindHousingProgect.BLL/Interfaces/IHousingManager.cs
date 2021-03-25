using FindHousingProject.BLL.Models;
using FindHousingProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Interfaces
{
    /// <summary>
    /// Housing manager.
    /// </summary>
    public interface IHousingManager
    {
        /// <summary>
        /// Create housing.
        /// </summary>
        Task CreateAsync(HousingDto housingDto);

        /// <summary>
        /// Get information by housing using owner's email.
        /// </summary>
        /// <param name="email">User email.</param>
        Task<IEnumerable<Housing>> GetCurrentHousingsAsync(string email);

        /// <summary>
        /// Deletion by the owner (owner id) of the house (identification by id).
        /// </summary>
        Task DeleteAsync(string id, string userId);

        /// <summary>
        /// Get housing using owner's id.
        /// </summary>
        /// <param name="id">User email.</param>
        Task<HousingDto> GetHousingAsync(string id);

        /// <summary>
        /// Updating the owner(owner id) information on the house.
        /// </summary>
        Task UpdateHousingAsync(HousingDto housingDto, string userId);

        /// <summary>
        /// List of all houses.
        /// </summary>
        IEnumerable<Housing> GetAllHousings();

        /// <summary>
        /// Search for a home by name, location, or check-in / check-out date.
        /// </summary>
        Task<IEnumerable<Housing>> SearchHousingAsync(string userInput, DateTime? checkIn = null, DateTime? checkOut = null);
    }
}