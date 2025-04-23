using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public SinhVienRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SinhVien>> GetAllAsync()
        {
            return await _context.SinhViens.Include(sv => sv.Lop).ToListAsync();
        }

        public async Task<SinhVien> GetByIdAsync(string id)
        {
            return await _context.SinhViens.Include(sv => sv.Lop).SingleOrDefaultAsync(sv => sv.MaSV == id);
        }

        public async Task<bool> AddAsync(SinhVien sinhVien)
        {
            _context.SinhViens.Add(sinhVien);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SinhVien sinhVien)
        {
            _context.Entry(sinhVien).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null) return false;
            _context.SinhViens.Remove(sinhVien);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.SinhViens.AnyAsync(sv => sv.MaSV == id);
        }
    }
}