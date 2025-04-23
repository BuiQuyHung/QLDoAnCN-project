using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class TienDoRepository : ITienDoRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public TienDoRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TienDo>> GetAllAsync()
        {
            return await _context.TienDos.Include(td => td.DeTai).Include(td => td.SinhVien).ToListAsync();
        }

        public async Task<TienDo> GetByIdAsync(int id)
        {
            return await _context.TienDos.Include(td => td.DeTai).Include(td => td.SinhVien).SingleOrDefaultAsync(td => td.MaTienDo == id);
        }

        public async Task<bool> AddAsync(TienDo tienDo)
        {
            _context.TienDos.Add(tienDo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TienDo tienDo)
        {
            _context.Entry(tienDo).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tienDo = await _context.TienDos.FindAsync(id);
            if (tienDo == null) return false;
            _context.TienDos.Remove(tienDo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.TienDos.AnyAsync(td => td.MaTienDo == id);
        }

        public async Task<IEnumerable<TienDo>> GetByDeTaiIdAndSinhVienIdAsync(string maDeTai, string maSV)
        {
            return await _context.TienDos
                .Where(td => td.MaDeTai == maDeTai && td.MaSV == maSV)
                .Include(td => td.DeTai)
                .Include(td => td.SinhVien)
                .ToListAsync();
        }

        public async Task<IEnumerable<TienDo>> GetByDeTaiIdAsync(string maDeTai)
        {
            return await _context.TienDos
                .Where(td => td.MaDeTai == maDeTai)
                .Include(td => td.SinhVien)
                .ToListAsync();
        }

        public async Task<IEnumerable<TienDo>> GetBySinhVienIdAsync(string maSV)
        {
            return await _context.TienDos
                .Where(td => td.MaSV == maSV)
                .Include(td => td.DeTai)
                .ToListAsync();
        }
    }
}