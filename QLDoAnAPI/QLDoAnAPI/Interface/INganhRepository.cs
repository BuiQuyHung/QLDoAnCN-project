using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface INganhRepository
    {
        Task<IEnumerable<Nganh>> GetAllAsync();
        Task<Nganh> GetByIdAsync(string id);
        Task<bool> AddAsync(Nganh nganh);
        Task<bool> UpdateAsync(Nganh nganh);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}