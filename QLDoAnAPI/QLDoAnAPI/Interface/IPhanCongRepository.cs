using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IPhanCongRepository
    {
        Task<IEnumerable<PhanCong>> GetAllAsync();
        Task<PhanCong> GetByIdAsync(string maDeTai, string maSV);
        Task<bool> AddAsync(PhanCong phanCong);
        Task<bool> UpdateAsync(PhanCong phanCong); // Có thể không cần thiết cho bảng này
        Task<bool> DeleteAsync(string maDeTai, string maSV);
        Task<bool> ExistsAsync(string maDeTai, string maSV);
        Task<IEnumerable<PhanCong>> GetByDeTaiIdAsync(string maDeTai);
        Task<IEnumerable<PhanCong>> GetBySinhVienIdAsync(string maSV);
    }
}