using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IKhoaRepository
    {
        Task<IEnumerable<Khoa>> GetAllAsync();
        Task<Khoa> GetByIdAsync(string id);
        Task<bool> AddAsync(Khoa khoa);
        Task<bool> UpdateAsync(Khoa khoa);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}