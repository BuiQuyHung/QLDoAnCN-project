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
    public class ChuyenNganhsController : ControllerBase
    {
        private readonly IChuyenNganhRepository _chuyenNganhRepository;
        private readonly INganhRepository _nganhRepository; // Để kiểm tra sự tồn tại của Nganh

        public ChuyenNganhsController(IChuyenNganhRepository chuyenNganhRepository, INganhRepository nganhRepository)
        {
            _chuyenNganhRepository = chuyenNganhRepository;
            _nganhRepository = nganhRepository;
        }

        // GET: api/ChuyenNganhs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChuyenNganh>>> GetChuyenNganhs()
        {
            var chuyenNganhs = await _chuyenNganhRepository.GetAllAsync();
            return Ok(chuyenNganhs);
        }

        // GET: api/ChuyenNganhs/CNPM
        [HttpGet("{id}")]
        public async Task<ActionResult<ChuyenNganh>> GetChuyenNganh(string id)
        {
            var chuyenNganh = await _chuyenNganhRepository.GetByIdAsync(id);
            if (chuyenNganh == null)
            {
                return NotFound();
            }
            return Ok(chuyenNganh);
        }

        // POST: api/ChuyenNganhs
        [HttpPost]
        public async Task<ActionResult<ChuyenNganhDTO>> PostChuyenNganh(ChuyenNganhDTO chuyenNganhDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _nganhRepository.ExistsAsync(chuyenNganhDTO.MaNganh))
            {
                return BadRequest($"Không tồn tại ngành có mã: {chuyenNganhDTO.MaNganh}");
            }

            var chuyenNganh = new ChuyenNganh
            {
                MaChuyenNganh = chuyenNganhDTO.MaChuyenNganh,
                TenChuyenNganh = chuyenNganhDTO.TenChuyenNganh,
                MaNganh = chuyenNganhDTO.MaNganh
            };

            if (await _chuyenNganhRepository.AddAsync(chuyenNganh))
            {
                return CreatedAtAction(nameof(GetChuyenNganh), new { id = chuyenNganh.MaChuyenNganh }, chuyenNganhDTO);
            }
            return BadRequest("Không thể thêm chuyên ngành.");
        }

        // PUT: api/ChuyenNganhs/CNPM
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChuyenNganh(string id, ChuyenNganhDTO chuyenNganhDTO)
        {
            if (id != chuyenNganhDTO.MaChuyenNganh)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _chuyenNganhRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _nganhRepository.ExistsAsync(chuyenNganhDTO.MaNganh))
            {
                return BadRequest($"Không tồn tại ngành có mã: {chuyenNganhDTO.MaNganh}");
            }

            var chuyenNganh = new ChuyenNganh
            {
                MaChuyenNganh = chuyenNganhDTO.MaChuyenNganh,
                TenChuyenNganh = chuyenNganhDTO.TenChuyenNganh,
                MaNganh = chuyenNganhDTO.MaNganh
            };

            if (await _chuyenNganhRepository.UpdateAsync(chuyenNganh))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật chuyên ngành.");
        }

        // DELETE: api/ChuyenNganhs/CNPM
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChuyenNganh(string id)
        {
            if (!await _chuyenNganhRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _chuyenNganhRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa chuyên ngành.");
        }
    }
}