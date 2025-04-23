using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IBoMonRepository
    {
        Task<IEnumerable<BoMon>> GetAllAsync();
        Task<BoMon> GetByIdAsync(string id);
        Task<bool> AddAsync(BoMon boMon);
        Task<bool> UpdateAsync(BoMon boMon);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}