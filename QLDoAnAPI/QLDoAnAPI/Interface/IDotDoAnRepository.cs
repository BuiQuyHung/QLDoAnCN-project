using QLDoAnAPI.Models;

namespace QLDoAnAPI.Interfaces
{
    public interface IDotDoAnRepository
    {
        Task<IEnumerable<DotDoAn>> GetAllAsync();
        Task<DotDoAn> GetByIdAsync(string maDotDoAn);
        void Create(DotDoAn dotDoAn);
        void Update(DotDoAn dotDoAn);
        void Delete(DotDoAn dotDoAn);
        Task<bool> SaveChangesAsync();
        Task<bool> DotDoAnExistsAsync(string maDotDoAn);
    }
}