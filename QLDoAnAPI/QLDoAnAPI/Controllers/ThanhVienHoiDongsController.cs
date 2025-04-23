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
    public class ThanhVienHoiDongsController : ControllerBase
    {
        private readonly IThanhVienHoiDongRepository _thanhVienHoiDongRepository;
        private readonly IHoiDongRepository _hoiDongRepository;
        private readonly IGiangVienRepository _giangVienRepository;

        public ThanhVienHoiDongsController(IThanhVienHoiDongRepository thanhVienHoiDongRepository, IHoiDongRepository hoiDongRepository, IGiangVienRepository giangVienRepository)
        {
            _thanhVienHoiDongRepository = thanhVienHoiDongRepository;
            _hoiDongRepository = hoiDongRepository;
            _giangVienRepository = giangVienRepository;
        }

        // GET: api/ThanhVienHoiDongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThanhVienHoiDong>>> GetThanhVienHoiDongs()
        {
            var thanhVienHoiDongs = await _thanhVienHoiDongRepository.GetAllAsync();
            return Ok(thanhVienHoiDongs);
        }

        // GET: api/ThanhVienHoiDongs/HD01/GV01
        [HttpGet("{maHoiDong}/{maGV}")]
        public async Task<ActionResult<ThanhVienHoiDong>> GetThanhVienHoiDong(string maHoiDong, string maGV)
        {
            var thanhVienHoiDong = await _thanhVienHoiDongRepository.GetByIdAsync(maHoiDong, maGV);
            if (thanhVienHoiDong == null)
            {
                return NotFound();
            }
            return Ok(thanhVienHoiDong);
        }

        // GET: api/ThanhVienHoiDongs/ByHoiDong/HD01
        [HttpGet("ByHoiDong/{maHoiDong}")]
        public async Task<ActionResult<IEnumerable<ThanhVienHoiDong>>> GetThanhVienHoiDongByHoiDong(string maHoiDong)
        {
            var thanhVienHoiDongs = await _thanhVienHoiDongRepository.GetByHoiDongIdAsync(maHoiDong);
            return Ok(thanhVienHoiDongs);
        }

        // GET: api/ThanhVienHoiDongs/ByGiangVien/GV01
        [HttpGet("ByGiangVien/{maGV}")]
        public async Task<ActionResult<IEnumerable<ThanhVienHoiDong>>> GetThanhVienHoiDongByGiangVien(string maGV)
        {
            var thanhVienHoiDongs = await _thanhVienHoiDongRepository.GetByGiangVienIdAsync(maGV);
            return Ok(thanhVienHoiDongs);
        }

        // POST: api/ThanhVienHoiDongs
        [HttpPost]
        public async Task<ActionResult<ThanhVienHoiDongDTO>> PostThanhVienHoiDong(ThanhVienHoiDongDTO thanhVienHoiDongDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _hoiDongRepository.ExistsAsync(thanhVienHoiDongDTO.MaHoiDong))
            {
                return BadRequest($"Không tồn tại hội đồng có mã: {thanhVienHoiDongDTO.MaHoiDong}");
            }

            if (!await _giangVienRepository.ExistsAsync(thanhVienHoiDongDTO.MaGV))
            {
                return BadRequest($"Không tồn tại giảng viên có mã: {thanhVienHoiDongDTO.MaGV}");
            }

            if (await _thanhVienHoiDongRepository.ExistsAsync(thanhVienHoiDongDTO.MaHoiDong, thanhVienHoiDongDTO.MaGV))
            {
                return Conflict($"Giảng viên '{thanhVienHoiDongDTO.MaGV}' đã là thành viên của hội đồng '{thanhVienHoiDongDTO.MaHoiDong}'.");
            }

            var thanhVienHoiDong = new ThanhVienHoiDong
            {
                MaHoiDong = thanhVienHoiDongDTO.MaHoiDong,
                MaGV = thanhVienHoiDongDTO.MaGV,
                VaiTro = thanhVienHoiDongDTO.VaiTro
            };

            if (await _thanhVienHoiDongRepository.AddAsync(thanhVienHoiDong))
            {
                return CreatedAtAction(nameof(GetThanhVienHoiDong), new { maHoiDong = thanhVienHoiDong.MaHoiDong, maGV = thanhVienHoiDong.MaGV }, thanhVienHoiDong);
            }
            return BadRequest("Không thể thêm thành viên hội đồng.");
        }

        // PUT: api/ThanhVienHoiDongs/HD01/GV01
        [HttpPut("{maHoiDong}/{maGV}")]
        public async Task<IActionResult> PutThanhVienHoiDong(string maHoiDong, string maGV, ThanhVienHoiDongDTO thanhVienHoiDongDTO)
        {
            if (maHoiDong != thanhVienHoiDongDTO.MaHoiDong || maGV != thanhVienHoiDongDTO.MaGV)
            {
                return BadRequest("Mã hội đồng hoặc mã giảng viên trong URL không khớp với dữ liệu.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _hoiDongRepository.ExistsAsync(thanhVienHoiDongDTO.MaHoiDong))
            {
                return BadRequest($"Không tồn tại hội đồng có mã: {thanhVienHoiDongDTO.MaHoiDong}");
            }

            if (!await _giangVienRepository.ExistsAsync(thanhVienHoiDongDTO.MaGV))
            {
                return BadRequest($"Không tồn tại giảng viên có mã: {thanhVienHoiDongDTO.MaGV}");
            }

            var existingThanhVien = await _thanhVienHoiDongRepository.GetByIdAsync(maHoiDong, maGV);
            if (existingThanhVien == null)
            {
                return NotFound();
            }

            existingThanhVien.VaiTro = thanhVienHoiDongDTO.VaiTro;

            if (await _thanhVienHoiDongRepository.UpdateAsync(existingThanhVien))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật thành viên hội đồng.");
        }

        // DELETE: api/ThanhVienHoiDongs/HD01/GV01
        [HttpDelete("{maHoiDong}/{maGV}")]
        public async Task<IActionResult> DeleteThanhVienHoiDong(string maHoiDong, string maGV)
        {
            if (!await _thanhVienHoiDongRepository.ExistsAsync(maHoiDong, maGV))
            {
                return NotFound();
            }

            if (await _thanhVienHoiDongRepository.DeleteAsync(maHoiDong, maGV))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa thành viên hội đồng.");
        }
    }
}