using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class HoiDongRepository : IHoiDongRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public HoiDongRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HoiDong>> GetAllAsync()
        {
            return await _context.HoiDongs.ToListAsync();
        }

        public async Task<HoiDong> GetByIdAsync(string id)
        {
            return await _context.HoiDongs.FindAsync(id);
        }

        public async Task<bool> AddAsync(HoiDong hoiDong)
        {
            _context.HoiDongs.Add(hoiDong);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(HoiDong hoiDong)
        {
            _context.Entry(hoiDong).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var hoiDong = await _context.HoiDongs.FindAsync(id);
            if (hoiDong == null) return false;
            _context.HoiDongs.Remove(hoiDong);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.HoiDongs.AnyAsync(hd => hd.MaHoiDong == id);
        }
    }
}