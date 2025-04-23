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
    public class DanhGiasController : ControllerBase
    {
        private readonly IDanhGiaRepository _danhGiaRepository;
        private readonly IDeTaiRepository _deTaiRepository;
        private readonly IGiangVienRepository _giangVienRepository;

        public DanhGiasController(IDanhGiaRepository danhGiaRepository, IDeTaiRepository deTaiRepository, IGiangVienRepository giangVienRepository)
        {
            _danhGiaRepository = danhGiaRepository;
            _deTaiRepository = deTaiRepository;
            _giangVienRepository = giangVienRepository;
        }

        // GET: api/DanhGias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhGia>>> GetDanhGias()
        {
            var danhGias = await _danhGiaRepository.GetAllAsync();
            return Ok(danhGias);
        }

        // GET: api/DanhGias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhGia>> GetDanhGia(int id)
        {
            var danhGia = await _danhGiaRepository.GetByIdAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }
            return Ok(danhGia);
        }

        // GET: api/DanhGias/ByDeTai/DTCNTT01
        [HttpGet("ByDeTai/{maDeTai}")]
        public async Task<ActionResult<IEnumerable<DanhGia>>> GetDanhGiaByDeTai(string maDeTai)
        {
            var danhGias = await _danhGiaRepository.GetByDeTaiIdAsync(maDeTai);
            return Ok(danhGias);
        }

        // GET: api/DanhGias/ByGiangVien/GV01
        [HttpGet("ByGiangVien/{maGV}")]
        public async Task<ActionResult<IEnumerable<DanhGia>>> GetDanhGiaByGiangVien(string maGV)
        {
            var danhGias = await _danhGiaRepository.GetByGiangVienIdAsync(maGV);
            return Ok(danhGias);
        }

        // GET: api/DanhGias/DTCNTT01/GV01
        [HttpGet("{maDeTai}/{maGV}")]
        public async Task<ActionResult<DanhGia>> GetDanhGiaByDeTaiAndGiangVien(string maDeTai, string maGV)
        {
            var danhGia = await _danhGiaRepository.GetByDeTaiIdAndGiangVienIdAsync(maDeTai, maGV);
            if (danhGia == null)
            {
                return NotFound();
            }
            return Ok(danhGia);
        }

        // POST: api/DanhGias
        [HttpPost]
        public async Task<ActionResult<DanhGiaDTO>> PostDanhGia(DanhGiaDTO danhGiaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _deTaiRepository.ExistsAsync(danhGiaDTO.MaDeTai))
            {
                return BadRequest($"Không tồn tại đề tài có mã: {danhGiaDTO.MaDeTai}");
            }

            if (!await _giangVienRepository.ExistsAsync(danhGiaDTO.MaGV))
            {
                return BadRequest($"Không tồn tại giảng viên có mã: {danhGiaDTO.MaGV}");
            }

            var danhGia = new DanhGia
            {
                MaDeTai = danhGiaDTO.MaDeTai,
                MaGV = danhGiaDTO.MaGV,
                DiemSo = danhGiaDTO.DiemSo,
                NhanXet = danhGiaDTO.NhanXet
            };

            if (await _danhGiaRepository.AddAsync(danhGia))
            {
                return CreatedAtAction(nameof(GetDanhGia), new { id = danhGia.MaDanhGia }, danhGia);
            }
            return BadRequest("Không thể thêm đánh giá.");
        }

        // PUT: api/DanhGias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanhGia(int id, DanhGiaDTO danhGiaDTO)
        {
            if (id <= 0)
            {
                return BadRequest("Mã đánh giá không hợp lệ.");
            }

            var existingDanhGia = await _danhGiaRepository.GetByIdAsync(id);
            if (existingDanhGia == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _deTaiRepository.ExistsAsync(danhGiaDTO.MaDeTai))
            {
                return BadRequest($"Không tồn tại đề tài có mã: {danhGiaDTO.MaDeTai}");
            }

            if (!await _giangVienRepository.ExistsAsync(danhGiaDTO.MaGV))
            {
                return BadRequest($"Không tồn tại giảng viên có mã: {danhGiaDTO.MaGV}");
            }

            existingDanhGia.MaDeTai = danhGiaDTO.MaDeTai;
            existingDanhGia.MaGV = danhGiaDTO.MaGV;
            existingDanhGia.DiemSo = danhGiaDTO.DiemSo;
            existingDanhGia.NhanXet = danhGiaDTO.NhanXet;

            if (await _danhGiaRepository.UpdateAsync(existingDanhGia))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật đánh giá.");
        }

        // DELETE: api/DanhGias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhGia(int id)
        {
            if (!await _danhGiaRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _danhGiaRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa đánh giá.");
        }
    }
}