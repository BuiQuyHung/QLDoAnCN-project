using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IChuyenNganhRepository
    {
        Task<IEnumerable<ChuyenNganh>> GetAllAsync();
        Task<ChuyenNganh> GetByIdAsync(string id);
        Task<bool> AddAsync(ChuyenNganh chuyenNganh);
        Task<bool> UpdateAsync(ChuyenNganh chuyenNganh);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}