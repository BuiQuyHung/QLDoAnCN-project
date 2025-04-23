using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class NganhRepository : INganhRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public NganhRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nganh>> GetAllAsync()
        {
            return await _context.Nganhs.Include(n => n.BoMon).ToListAsync();
        }

        public async Task<Nganh> GetByIdAsync(string id)
        {
            return await _context.Nganhs.Include(n => n.BoMon).SingleOrDefaultAsync(n => n.MaNganh == id);
        }

        public async Task<bool> AddAsync(Nganh nganh)
        {
            _context.Nganhs.Add(nganh);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Nganh nganh)
        {
            _context.Entry(nganh).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var nganh = await _context.Nganhs.FindAsync(id);
            if (nganh == null) return false;
            _context.Nganhs.Remove(nganh);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Nganhs.AnyAsync(n => n.MaNganh == id);
        }
    }
}