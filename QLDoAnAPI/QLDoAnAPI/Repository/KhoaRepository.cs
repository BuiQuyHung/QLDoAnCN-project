// Repositories/KhoaRepository.cs
using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class KhoaRepository : IKhoaRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public KhoaRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Khoa>> GetAllAsync()
        {
            return await _context.Khoas.ToListAsync();
        }

        public async Task<Khoa> GetByIdAsync(string id)
        {
            return await _context.Khoas.FindAsync(id);
        }

        public async Task<bool> AddAsync(Khoa khoa)
        {
            _context.Khoas.Add(khoa);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Khoa khoa)
        {
            _context.Entry(khoa).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null) return false;
            _context.Khoas.Remove(khoa);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Khoas.AnyAsync(k => k.MaKhoa == id);
        }
    }
}