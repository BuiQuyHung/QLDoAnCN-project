using Microsoft.AspNetCore.Mvc;
using QLDoAnAPI.DTOs;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDoAnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongsController : ControllerBase
    {
        private readonly IPhanCongRepository _phanCongRepository;
        private readonly IDeTaiRepository _deTaiRepository;
        private readonly ISinhVienRepository _sinhVienRepository;

        public PhanCongsController(IPhanCongRepository phanCongRepository, IDeTaiRepository deTaiRepository, ISinhVienRepository sinhVienRepository)
        {
            _phanCongRepository = phanCongRepository;
            _deTaiRepository = deTaiRepository;
            _sinhVienRepository = sinhVienRepository;
        }

        // GET: api/PhanCongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanCong>>> GetPhanCongs()
        {
            var phanCongs = await _phanCongRepository.GetAllAsync();
            return Ok(phanCongs);
        }

        // GET: api/PhanCongs/DT001/SV001
        [HttpGet("{maDeTai}/{maSV}")]
        public async Task<ActionResult<PhanCong>> GetPhanCong(string maDeTai, string maSV)
        {
            var phanCong = await _phanCongRepository.GetByIdAsync(maDeTai, maSV);
            if (phanCong == null)
            {
                return NotFound();
            }
            return Ok(phanCong);
        }

        // GET: api/PhanCongs/ByDeTai/DT001
        [HttpGet("ByDeTai/{maDeTai}")]
        public async Task<ActionResult<IEnumerable<PhanCong>>> GetPhanCongByDeTai(string maDeTai)
        {
            var phanCongs = await _phanCongRepository.GetByDeTaiIdAsync(maDeTai);
            return Ok(phanCongs);
        }

        // GET: api/PhanCongs/BySinhVien/SV001
        [HttpGet("BySinhVien/{maSV}")]
        public async Task<ActionResult<IEnumerable<PhanCong>>> GetPhanCongBySinhVien(string maSV)
        {
            var phanCongs = await _phanCongRepository.GetBySinhVienIdAsync(maSV);
            return Ok(phanCongs);
        }

        // POST: api/PhanCongs
        [HttpPost]
        public async Task<ActionResult<PhanCongDTO>> PostPhanCong(PhanCongDTO phanCongDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _deTaiRepository.ExistsAsync(phanCongDTO.MaDeTai))
            {
                return BadRequest($"Không tồn tại đề tài có mã: {phanCongDTO.MaDeTai}");
            }

            if (!await _sinhVienRepository.ExistsAsync(phanCongDTO.MaSV))
            {
                return BadRequest($"Không tồn tại sinh viên có mã: {phanCongDTO.MaSV}");
            }

            var phanCong = new PhanCong
            {
                MaDeTai = phanCongDTO.MaDeTai,
                MaSV = phanCongDTO.MaSV,
                NgayPhanCong = DateTime.Now // Hoặc để EF Core tự xử lý
            };

            if (await _phanCongRepository.AddAsync(phanCong))
            {
                return CreatedAtAction(nameof(GetPhanCong), new { maDeTai = phanCong.MaDeTai, maSV = phanCong.MaSV }, phanCongDTO);
            }
            return BadRequest("Không thể thêm phân công.");
        }

        // DELETE: api/PhanCongs/DT001/SV001
        [HttpDelete("{maDeTai}/{maSV}")]
        public async Task<IActionResult> DeletePhanCong(string maDeTai, string maSV)
        {
            if (!await _phanCongRepository.ExistsAsync(maDeTai, maSV))
            {
                return NotFound();
            }

            if (await _phanCongRepository.DeleteAsync(maDeTai, maSV))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa phân công.");
        }
    }
}