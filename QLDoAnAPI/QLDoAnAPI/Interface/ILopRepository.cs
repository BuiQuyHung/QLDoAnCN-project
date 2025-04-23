using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface ILopRepository
    {
        Task<IEnumerable<Lop>> GetAllAsync();
        Task<Lop> GetByIdAsync(string id);
        Task<bool> AddAsync(Lop lop);
        Task<bool> UpdateAsync(Lop lop);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}