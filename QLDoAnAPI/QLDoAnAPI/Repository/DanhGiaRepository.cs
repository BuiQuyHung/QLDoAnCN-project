using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class DanhGiaRepository : IDanhGiaRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public DanhGiaRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DanhGia>> GetAllAsync()
        {
            return await _context.DanhGias.Include(dg => dg.DeTai).Include(dg => dg.GiangVien).ToListAsync();
        }

        public async Task<DanhGia> GetByIdAsync(int id)
        {
            return await _context.DanhGias.Include(dg => dg.DeTai).Include(dg => dg.GiangVien).SingleOrDefaultAsync(dg => dg.MaDanhGia == id);
        }

        public async Task<bool> AddAsync(DanhGia danhGia)
        {
            _context.DanhGias.Add(danhGia);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DanhGia danhGia)
        {
            _context.Entry(danhGia).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var danhGia = await _context.DanhGias.FindAsync(id);
            if (danhGia == null) return false;
            _context.DanhGias.Remove(danhGia);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.DanhGias.AnyAsync(dg => dg.MaDanhGia == id);
        }

        public async Task<IEnumerable<DanhGia>> GetByDeTaiIdAsync(string maDeTai)
        {
            return await _context.DanhGias
                .Where(dg => dg.MaDeTai == maDeTai)
                .Include(dg => dg.GiangVien)
                .ToListAsync();
        }

        public async Task<IEnumerable<DanhGia>> GetByGiangVienIdAsync(string maGV)
        {
            return await _context.DanhGias
                .Where(dg => dg.MaGV == maGV)
                .Include(dg => dg.DeTai)
                .ToListAsync();
        }

        public async Task<DanhGia> GetByDeTaiIdAndGiangVienIdAsync(string maDeTai, string maGV)
        {
            return await _context.DanhGias
                .Where(dg => dg.MaDeTai == maDeTai && dg.MaGV == maGV)
                .Include(dg => dg.DeTai)
                .Include(dg => dg.GiangVien)
                .FirstOrDefaultAsync();
        }
    }
}