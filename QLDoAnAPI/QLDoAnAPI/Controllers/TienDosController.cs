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
    public class TienDosController : ControllerBase
    {
        private readonly ITienDoRepository _tienDoRepository;
        private readonly IDeTaiRepository _deTaiRepository;
        private readonly ISinhVienRepository _sinhVienRepository;

        public TienDosController(ITienDoRepository tienDoRepository, IDeTaiRepository deTaiRepository, ISinhVienRepository sinhVienRepository)
        {
            _tienDoRepository = tienDoRepository;
            _deTaiRepository = deTaiRepository;
            _sinhVienRepository = sinhVienRepository;
        }

        // GET: api/TienDos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TienDo>>> GetTienDos()
        {
            var tienDos = await _tienDoRepository.GetAllAsync();
            return Ok(tienDos);
        }

        // GET: api/TienDos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TienDo>> GetTienDo(int id)
        {
            var tienDo = await _tienDoRepository.GetByIdAsync(id);
            if (tienDo == null)
            {
                return NotFound();
            }
            return Ok(tienDo);
        }

        // GET: api/TienDos/ByDeTaiAndSinhVien/DTCNTT01/SV06
        [HttpGet("ByDeTaiAndSinhVien/{maDeTai}/{maSV}")]
        public async Task<ActionResult<IEnumerable<TienDo>>> GetTienDoByDeTaiAndSinhVien(string maDeTai, string maSV)
        {
            var tienDos = await _tienDoRepository.GetByDeTaiIdAndSinhVienIdAsync(maDeTai, maSV);
            return Ok(tienDos);
        }

        // GET: api/TienDos/ByDeTai/DTCNTT01
        [HttpGet("ByDeTai/{maDeTai}")]
        public async Task<ActionResult<IEnumerable<TienDo>>> GetTienDoByDeTai(string maDeTai)
        {
            var tienDos = await _tienDoRepository.GetByDeTaiIdAsync(maDeTai);
            return Ok(tienDos);
        }

        // GET: api/TienDos/BySinhVien/SV06
        [HttpGet("BySinhVien/{maSV}")]
        public async Task<ActionResult<IEnumerable<TienDo>>> GetTienDoBySinhVien(string maSV)
        {
            var tienDos = await _tienDoRepository.GetBySinhVienIdAsync(maSV);
            return Ok(tienDos);
        }

        // POST: api/TienDos
        [HttpPost]
        public async Task<ActionResult<TienDoDTO>> PostTienDo(TienDoDTO tienDoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _deTaiRepository.ExistsAsync(tienDoDTO.MaDeTai))
            {
                return BadRequest($"Không tồn tại đề tài có mã: {tienDoDTO.MaDeTai}");
            }

            if (!await _sinhVienRepository.ExistsAsync(tienDoDTO.MaSV))
            {
                return BadRequest($"Không tồn tại sinh viên có mã: {tienDoDTO.MaSV}");
            }

            var tienDo = new TienDo
            {
                MaDeTai = tienDoDTO.MaDeTai,
                MaSV = tienDoDTO.MaSV,
                NgayNop = tienDoDTO.NgayNop,
                LoaiBaoCao = tienDoDTO.LoaiBaoCao,
                TepDinhKem = tienDoDTO.TepDinhKem,
                GhiChu = tienDoDTO.GhiChu
            };

            if (await _tienDoRepository.AddAsync(tienDo))
            {
                return CreatedAtAction(nameof(GetTienDo), new { id = tienDo.MaTienDo }, tienDo);
            }
            return BadRequest("Không thể thêm tiến độ.");
        }

        // PUT: api/TienDos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTienDo(int id, TienDoDTO tienDoDTO)
        {
            if (id <= 0) // Kiểm tra ID hợp lệ
            {
                return BadRequest("Mã tiến độ không hợp lệ.");
            }

            var existingTienDo = await _tienDoRepository.GetByIdAsync(id);
            if (existingTienDo == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _deTaiRepository.ExistsAsync(tienDoDTO.MaDeTai))
            {
                return BadRequest($"Không tồn tại đề tài có mã: {tienDoDTO.MaDeTai}");
            }

            if (!await _sinhVienRepository.ExistsAsync(tienDoDTO.MaSV))
            {
                return BadRequest($"Không tồn tại sinh viên có mã: {tienDoDTO.MaSV}");
            }

            // Cập nhật các thuộc tính của existingTienDo từ DTO
            existingTienDo.MaDeTai = tienDoDTO.MaDeTai;
            existingTienDo.MaSV = tienDoDTO.MaSV;
            existingTienDo.NgayNop = tienDoDTO.NgayNop;
            existingTienDo.LoaiBaoCao = tienDoDTO.LoaiBaoCao;
            existingTienDo.TepDinhKem = tienDoDTO.TepDinhKem;
            existingTienDo.GhiChu = tienDoDTO.GhiChu;

            if (await _tienDoRepository.UpdateAsync(existingTienDo))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật tiến độ.");
        }

        // DELETE: api/TienDos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTienDo(int id)
        {
            if (!await _tienDoRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _tienDoRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa tiến độ.");
        }
    }
}