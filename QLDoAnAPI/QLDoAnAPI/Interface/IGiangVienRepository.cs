using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IGiangVienRepository
    {
        Task<IEnumerable<GiangVien>> GetAllAsync();
        Task<GiangVien> GetByIdAsync(string id);
        Task<bool> AddAsync(GiangVien giangVien);
        Task<bool> UpdateAsync(GiangVien giangVien);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}