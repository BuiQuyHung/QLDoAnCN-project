using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface ISinhVienRepository
    {
        Task<IEnumerable<SinhVien>> GetAllAsync();
        Task<SinhVien> GetByIdAsync(string id);
        Task<bool> AddAsync(SinhVien sinhVien);
        Task<bool> UpdateAsync(SinhVien sinhVien);
        Task<bool> DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}