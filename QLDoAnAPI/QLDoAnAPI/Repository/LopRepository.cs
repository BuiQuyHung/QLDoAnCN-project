using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class LopRepository : ILopRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public LopRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lop>> GetAllAsync()
        {
            return await _context.Lops.Include(l => l.ChuyenNganh).ToListAsync();
        }

        public async Task<Lop> GetByIdAsync(string id)
        {
            return await _context.Lops.Include(l => l.ChuyenNganh).SingleOrDefaultAsync(l => l.MaLop == id);
        }

        public async Task<bool> AddAsync(Lop lop)
        {
            _context.Lops.Add(lop);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Lop lop)
        {
            _context.Entry(lop).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var lop = await _context.Lops.FindAsync(id);
            if (lop == null) return false;
            _context.Lops.Remove(lop);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Lops.AnyAsync(l => l.MaLop == id);
        }
    }
}