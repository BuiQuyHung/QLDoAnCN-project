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
    public class LopsController : ControllerBase
    {
        private readonly ILopRepository _lopRepository;
        private readonly IChuyenNganhRepository _chuyenNganhRepository; // Để kiểm tra sự tồn tại của ChuyenNganh

        public LopsController(ILopRepository lopRepository, IChuyenNganhRepository chuyenNganhRepository)
        {
            _lopRepository = lopRepository;
            _chuyenNganhRepository = chuyenNganhRepository;
        }

        // GET: api/Lops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lop>>> GetLops()
        {
            var lops = await _lopRepository.GetAllAsync();
            return Ok(lops);
        }

        // GET: api/Lops/D18CQCNPM01
        [HttpGet("{id}")]
        public async Task<ActionResult<Lop>> GetLop(string id)
        {
            var lop = await _lopRepository.GetByIdAsync(id);
            if (lop == null)
            {
                return NotFound();
            }
            return Ok(lop);
        }

        // POST: api/Lops
        [HttpPost]
        public async Task<ActionResult<LopDTO>> PostLop(LopDTO lopDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _chuyenNganhRepository.ExistsAsync(lopDTO.MaChuyenNganh))
            {
                return BadRequest($"Không tồn tại chuyên ngành có mã: {lopDTO.MaChuyenNganh}");
            }

            var lop = new Lop
            {
                MaLop = lopDTO.MaLop,
                TenLop = lopDTO.TenLop,
                KhoaHoc = lopDTO.KhoaHoc,
                MaChuyenNganh = lopDTO.MaChuyenNganh
            };

            if (await _lopRepository.AddAsync(lop))
            {
                return CreatedAtAction(nameof(GetLop), new { id = lop.MaLop }, lopDTO);
            }
            return BadRequest("Không thể thêm lớp.");
        }

        // PUT: api/Lops/D18CQCNPM01
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLop(string id, LopDTO lopDTO)
        {
            if (id != lopDTO.MaLop)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _lopRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _chuyenNganhRepository.ExistsAsync(lopDTO.MaChuyenNganh))
            {
                return BadRequest($"Không tồn tại chuyên ngành có mã: {lopDTO.MaChuyenNganh}");
            }

            var lop = new Lop
            {
                MaLop = lopDTO.MaLop,
                TenLop = lopDTO.TenLop,
                KhoaHoc = lopDTO.KhoaHoc,
                MaChuyenNganh = lopDTO.MaChuyenNganh
            };

            if (await _lopRepository.UpdateAsync(lop))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật lớp.");
        }

        // DELETE: api/Lops/D18CQCNPM01
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLop(string id)
        {
            if (!await _lopRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _lopRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa lớp.");
        }
    }
}