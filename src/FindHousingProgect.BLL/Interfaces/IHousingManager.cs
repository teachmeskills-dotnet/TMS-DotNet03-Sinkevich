using FindHousingProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Interfaces
{
    public interface IHousingManager
    {
        Task CreateAsync(Housing housingDto);
        // Task GetHousingAsync();
        Task<IEnumerable<Housing>> GetCurrentHousingsAsync(string email);
    }
}