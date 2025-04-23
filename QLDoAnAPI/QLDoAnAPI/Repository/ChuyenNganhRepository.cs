using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class ChuyenNganhRepository : IChuyenNganhRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public ChuyenNganhRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChuyenNganh>> GetAllAsync()
        {
            return await _context.ChuyenNganhs.Include(cn => cn.Nganh).ToListAsync();
        }

        public async Task<ChuyenNganh> GetByIdAsync(string id)
        {
            return await _context.ChuyenNganhs.Include(cn => cn.Nganh).SingleOrDefaultAsync(cn => cn.MaChuyenNganh == id);
        }

        public async Task<bool> AddAsync(ChuyenNganh chuyenNganh)
        {
            _context.ChuyenNganhs.Add(chuyenNganh);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ChuyenNganh chuyenNganh)
        {
            _context.Entry(chuyenNganh).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var chuyenNganh = await _context.ChuyenNganhs.FindAsync(id);
            if (chuyenNganh == null) return false;
            _context.ChuyenNganhs.Remove(chuyenNganh);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.ChuyenNganhs.AnyAsync(cn => cn.MaChuyenNganh == id);
        }
    }
}