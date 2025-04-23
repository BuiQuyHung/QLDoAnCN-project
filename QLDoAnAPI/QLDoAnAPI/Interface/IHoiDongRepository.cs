using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IHoiDongRepository
    {
        Task<IEnumerable<HoiDong>> GetAllAsync();
        Task<HoiDong> GetByIdAsync(string id);
        Task<bool> AddAsync(HoiDong hoiDong);
        Task<bool> UpdateAsync(HoiDong hoiDong);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}