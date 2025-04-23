using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class PhanCongRepository : IPhanCongRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public PhanCongRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PhanCong>> GetAllAsync()
        {
            return await _context.PhanCongs.Include(pc => pc.DeTai).Include(pc => pc.SinhVien).ToListAsync();
        }

        public async Task<PhanCong> GetByIdAsync(string maDeTai, string maSV)
        {
            return await _context.PhanCongs
                .Include(pc => pc.DeTai)
                .Include(pc => pc.SinhVien)
                .SingleOrDefaultAsync(pc => pc.MaDeTai == maDeTai && pc.MaSV == maSV);
        }

        public async Task<bool> AddAsync(PhanCong phanCong)
        {
            _context.PhanCongs.Add(phanCong);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(PhanCong phanCong)
        {
            _context.Entry(phanCong).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string maDeTai, string maSV)
        {
            var phanCong = await _context.PhanCongs.FindAsync(maDeTai, maSV);
            if (phanCong == null) return false;
            _context.PhanCongs.Remove(phanCong);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string maDeTai, string maSV)
        {
            return await _context.PhanCongs.AnyAsync(pc => pc.MaDeTai == maDeTai && pc.MaSV == maSV);
        }

        public async Task<IEnumerable<PhanCong>> GetByDeTaiIdAsync(string maDeTai)
        {
            return await _context.PhanCongs
                .Where(pc => pc.MaDeTai == maDeTai)
                .Include(pc => pc.SinhVien)
                .ToListAsync();
        }

        public async Task<IEnumerable<PhanCong>> GetBySinhVienIdAsync(string maSV)
        {
            return await _context.PhanCongs
                .Where(pc => pc.MaSV == maSV)
                .Include(pc => pc.DeTai)
                .ToListAsync();
        }
    }
}