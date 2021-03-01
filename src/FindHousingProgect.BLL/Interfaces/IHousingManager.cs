using FindHousingProject.BLL.Models;
using FindHousingProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Interfaces
{
    public interface IHousingManager
    {
        Task CreateAsync(HousingDto housingDto);
        // Task GetHousingAsync();
        Task<IEnumerable<Housing>> GetCurrentHousingsAsync(string email);
        Task DeleteAsync(string id, string userId);
        Task<HousingDto> GetHousingAsync(string id);
        Task UpdateHousingAsync(HousingDto housingDto, string userId);
    }
}