using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Interfaces
{
    public interface ITienDoRepository
    {
        Task<IEnumerable<TienDo>> GetAllAsync();
        Task<TienDo> GetByIdAsync(int id);
        Task<bool> AddAsync(TienDo tienDo);
        Task<bool> UpdateAsync(TienDo tienDo);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<TienDo>> GetByDeTaiIdAndSinhVienIdAsync(string maDeTai, string maSV);
        Task<IEnumerable<TienDo>> GetByDeTaiIdAsync(string maDeTai);
        Task<IEnumerable<TienDo>> GetBySinhVienIdAsync(string maSV);
    }
}