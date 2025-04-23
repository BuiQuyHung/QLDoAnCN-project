using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IDanhGiaRepository
    {
        Task<IEnumerable<DanhGia>> GetAllAsync();
        Task<DanhGia> GetByIdAsync(int id);
        Task<bool> AddAsync(DanhGia danhGia);
        Task<bool> UpdateAsync(DanhGia danhGia);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<DanhGia>> GetByDeTaiIdAsync(string maDeTai);
        Task<IEnumerable<DanhGia>> GetByGiangVienIdAsync(string maGV);
        Task<DanhGia> GetByDeTaiIdAndGiangVienIdAsync(string maDeTai, string maGV);
    }
}