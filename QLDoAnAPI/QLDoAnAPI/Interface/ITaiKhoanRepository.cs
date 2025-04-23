using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface ITaiKhoanRepository
    {
        Task<IEnumerable<TaiKhoan>> GetAllAsync();
        Task<TaiKhoan> GetByIdAsync(string tenDangNhap);
        Task<bool> AddAsync(TaiKhoan taiKhoan);
        Task<bool> UpdateAsync(TaiKhoan taiKhoan);
        Task<bool> DeleteAsync(string tenDangNhap);
        Task<bool> ExistsAsync(string tenDangNhap);
        Task<TaiKhoan> GetByMaNguoiDungAsync(string maNguoiDung);
    }
}