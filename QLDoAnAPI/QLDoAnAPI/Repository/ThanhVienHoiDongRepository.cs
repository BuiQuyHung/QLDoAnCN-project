using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class ThanhVienHoiDongRepository : IThanhVienHoiDongRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public ThanhVienHoiDongRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ThanhVienHoiDong>> GetAllAsync()
        {
            return await _context.ThanhVienHoiDongs.Include(tv => tv.HoiDong).Include(tv => tv.GiangVien).ToListAsync();
        }

        public async Task<ThanhVienHoiDong> GetByIdAsync(string maHoiDong, string maGV)
        {
            return await _context.ThanhVienHoiDongs
                .Include(tv => tv.HoiDong)
                .Include(tv => tv.GiangVien)
                .SingleOrDefaultAsync(tv => tv.MaHoiDong == maHoiDong && tv.MaGV == maGV);
        }

        public async Task<bool> AddAsync(ThanhVienHoiDong thanhVienHoiDong)
        {
            _context.ThanhVienHoiDongs.Add(thanhVienHoiDong);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ThanhVienHoiDong thanhVienHoiDong)
        {
            _context.Entry(thanhVienHoiDong).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string maHoiDong, string maGV)
        {
            var thanhVien = await _context.ThanhVienHoiDongs.FindAsync(maHoiDong, maGV);
            if (thanhVien == null) return false;
            _context.ThanhVienHoiDongs.Remove(thanhVien);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string maHoiDong, string maGV)
        {
            return await _context.ThanhVienHoiDongs.AnyAsync(tv => tv.MaHoiDong == maHoiDong && tv.MaGV == maGV);
        }

        public async Task<IEnumerable<ThanhVienHoiDong>> GetByHoiDongIdAsync(string maHoiDong)
        {
            return await _context.ThanhVienHoiDongs
                .Where(tv => tv.MaHoiDong == maHoiDong)
                .Include(tv => tv.GiangVien)
                .ToListAsync();
        }

        public async Task<IEnumerable<ThanhVienHoiDong>> GetByGiangVienIdAsync(string maGV)
        {
            return await _context.ThanhVienHoiDongs
                .Where(tv => tv.MaGV == maGV)
                .Include(tv => tv.HoiDong)
                .ToListAsync();
        }
    }
}