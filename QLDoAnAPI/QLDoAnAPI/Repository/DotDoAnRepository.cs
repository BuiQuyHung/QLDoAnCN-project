using Microsoft.EntityFrameworkCore;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using QLDoAnAPI.Data; 

namespace YourNamespace.Repository
{
    public class DotDoAnRepository : IDotDoAnRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public DotDoAnRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DotDoAn>> GetAllAsync()
        {
            return await _context.DotDoAns.ToListAsync();
        }

        public async Task<DotDoAn> GetByIdAsync(string maDotDoAn)
        {
            return await _context.DotDoAns.FindAsync(maDotDoAn);
        }

        public void Create(DotDoAn dotDoAn)
        {
            _context.DotDoAns.Add(dotDoAn);
        }

        public void Update(DotDoAn dotDoAn)
        {
            _context.DotDoAns.Update(dotDoAn);
        }

        public void Delete(DotDoAn dotDoAn)
        {
            _context.DotDoAns.Remove(dotDoAn);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DotDoAnExistsAsync(string maDotDoAn)
        {
            return await _context.DotDoAns.AnyAsync(d => d.MaDotDoAn == maDotDoAn);
        }
    }
}