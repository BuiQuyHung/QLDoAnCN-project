// Controllers/NganhsController.cs
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
    public class NganhsController : ControllerBase
    {
        private readonly INganhRepository _nganhRepository;
        private readonly IBoMonRepository _boMonRepository; // Để kiểm tra sự tồn tại của BoMon

        public NganhsController(INganhRepository nganhRepository, IBoMonRepository boMonRepository)
        {
            _nganhRepository = nganhRepository;
            _boMonRepository = boMonRepository;
        }

        // GET: api/Nganhs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nganh>>> GetNganhs()
        {
            var nganhs = await _nganhRepository.GetAllAsync();
            return Ok(nganhs);
        }

        // GET: api/Nganhs/CNPM
        [HttpGet("{id}")]
        public async Task<ActionResult<Nganh>> GetNganh(string id)
        {
            var nganh = await _nganhRepository.GetByIdAsync(id);
            if (nganh == null)
            {
                return NotFound();
            }
            return Ok(nganh);
        }

        // POST: api/Nganhs
        [HttpPost]
        public async Task<ActionResult<NganhDTO>> PostNganh(NganhDTO nganhDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _boMonRepository.ExistsAsync(nganhDTO.MaBoMon))
            {
                return BadRequest($"Không tồn tại bộ môn có mã: {nganhDTO.MaBoMon}");
            }

            var nganh = new Nganh
            {
                MaNganh = nganhDTO.MaNganh,
                TenNganh = nganhDTO.TenNganh,
                MaBoMon = nganhDTO.MaBoMon
            };

            if (await _nganhRepository.AddAsync(nganh))
            {
                return CreatedAtAction(nameof(GetNganh), new { id = nganh.MaNganh }, nganhDTO);
            }
            return BadRequest("Không thể thêm ngành.");
        }

        // PUT: api/Nganhs/CNPM
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNganh(string id, NganhDTO nganhDTO)
        {
            if (id != nganhDTO.MaNganh)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _nganhRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _boMonRepository.ExistsAsync(nganhDTO.MaBoMon))
            {
                return BadRequest($"Không tồn tại bộ môn có mã: {nganhDTO.MaBoMon}");
            }

            var nganh = new Nganh
            {
                MaNganh = nganhDTO.MaNganh,
                TenNganh = nganhDTO.TenNganh,
                MaBoMon = nganhDTO.MaBoMon
            };

            if (await _nganhRepository.UpdateAsync(nganh))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật ngành.");
        }

        // DELETE: api/Nganhs/CNPM
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNganh(string id)
        {
            if (!await _nganhRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _nganhRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa ngành.");
        }
    }
}