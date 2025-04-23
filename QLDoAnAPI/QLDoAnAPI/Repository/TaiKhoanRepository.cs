using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public TaiKhoanRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaiKhoan>> GetAllAsync()
        {
            return await _context.TaiKhoans.ToListAsync();
        }

        public async Task<TaiKhoan> GetByIdAsync(string tenDangNhap)
        {
            return await _context.TaiKhoans.FindAsync(tenDangNhap);
        }

        public async Task<bool> AddAsync(TaiKhoan taiKhoan)
        {
            _context.TaiKhoans.Add(taiKhoan);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TaiKhoan taiKhoan)
        {
            _context.Entry(taiKhoan).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string tenDangNhap)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(tenDangNhap);
            if (taiKhoan == null) return false;
            _context.TaiKhoans.Remove(taiKhoan);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string tenDangNhap)
        {
            return await _context.TaiKhoans.AnyAsync(tk => tk.TenDangNhap == tenDangNhap);
        }

        public async Task<TaiKhoan> GetByMaNguoiDungAsync(string maNguoiDung)
        {
            return await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.MaNguoiDung == maNguoiDung);
        }
    }
}