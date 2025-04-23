using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class DeTaiRepository : IDeTaiRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public DeTaiRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeTai>> GetAllAsync()
        {
            return await _context.DeTais.Include(dt => dt.GiangVien).ToListAsync();
        }

        public async Task<DeTai> GetByIdAsync(string id)
        {
            return await _context.DeTais.Include(dt => dt.GiangVien).SingleOrDefaultAsync(dt => dt.MaDeTai == id);
        }

        public async Task<bool> AddAsync(DeTai deTai)
        {
            _context.DeTais.Add(deTai);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DeTai deTai)
        {
            _context.Entry(deTai).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deTai = await _context.DeTais.FindAsync(id);
            if (deTai == null) return false;
            _context.DeTais.Remove(deTai);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.DeTais.AnyAsync(dt => dt.MaDeTai == id);
        }
    }
}