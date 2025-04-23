using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface IThanhVienHoiDongRepository
    {
        Task<IEnumerable<ThanhVienHoiDong>> GetAllAsync();
        Task<ThanhVienHoiDong> GetByIdAsync(string maHoiDong, string maGV);
        Task<bool> AddAsync(ThanhVienHoiDong thanhVienHoiDong);
        Task<bool> UpdateAsync(ThanhVienHoiDong thanhVienHoiDong); 
        Task<bool> DeleteAsync(string maHoiDong, string maGV);
        Task<bool> ExistsAsync(string maHoiDong, string maGV);
        Task<IEnumerable<ThanhVienHoiDong>> GetByHoiDongIdAsync(string maHoiDong);
        Task<IEnumerable<ThanhVienHoiDong>> GetByGiangVienIdAsync(string maGV);
    }
}