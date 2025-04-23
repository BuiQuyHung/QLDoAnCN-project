// Repositories/BoMonRepository.cs
using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Repositories
{
    public class BoMonRepository : IBoMonRepository
    {
        private readonly QLDoAnChuyenNganhDbContext _context;

        public BoMonRepository(QLDoAnChuyenNganhDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BoMon>> GetAllAsync()
        {
            return await _context.BoMons.Include(bm => bm.Khoa).ToListAsync();
        }

        public async Task<BoMon> GetByIdAsync(string id)
        {
            return await _context.BoMons.Include(bm => bm.Khoa).SingleOrDefaultAsync(bm => bm.MaBoMon == id);
        }

        public async Task<bool> AddAsync(BoMon boMon)
        {
            _context.BoMons.Add(boMon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BoMon boMon)
        {
            _context.Entry(boMon).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var boMon = await _context.BoMons.FindAsync(id);
            if (boMon == null) return false;
            _context.BoMons.Remove(boMon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.BoMons.AnyAsync(bm => bm.MaBoMon == id);
        }
    }
}