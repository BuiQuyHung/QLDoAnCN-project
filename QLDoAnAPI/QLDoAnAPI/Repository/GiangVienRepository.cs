using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class GiangVienRepository : IGiangVienRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public GiangVienRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GiangVien>> GetAllAsync()
        {
            return await _context.GiangViens.ToListAsync();
        }

        public async Task<GiangVien> GetByIdAsync(string id)
        {
            return await _context.GiangViens.FindAsync(id);
        }

        public async Task<bool> AddAsync(GiangVien giangVien)
        {
            _context.GiangViens.Add(giangVien);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(GiangVien giangVien)
        {
            _context.Entry(giangVien).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien == null) return false;
            _context.GiangViens.Remove(giangVien);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.GiangViens.AnyAsync(gv => gv.MaGV == id);
        }
    }
}