using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IDeTaiRepository
    {
        Task<IEnumerable<DeTai>> GetAllAsync();
        Task<DeTai> GetByIdAsync(string id);
        Task<bool> AddAsync(DeTai deTai);
        Task<bool> UpdateAsync(DeTai deTai);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}