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
    public class BoMonsController : ControllerBase
    {
        private readonly IBoMonRepository _boMonRepository;
        private readonly IKhoaRepository _khoaRepository; // Để kiểm tra sự tồn tại của Khoa

        public BoMonsController(IBoMonRepository boMonRepository, IKhoaRepository khoaRepository)
        {
            _boMonRepository = boMonRepository;
            _khoaRepository = khoaRepository;
        }

        // GET: api/BoMons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoMon>>> GetBoMons()
        {
            var boMons = await _boMonRepository.GetAllAsync();
            return Ok(boMons);
        }

        // GET: api/BoMons/BM001
        [HttpGet("{id}")]
        public async Task<ActionResult<BoMon>> GetBoMon(string id)
        {
            var boMon = await _boMonRepository.GetByIdAsync(id);
            if (boMon == null)
            {
                return NotFound();
            }
            return Ok(boMon);
        }

        // POST: api/BoMons
        [HttpPost]
        public async Task<ActionResult<BoMonDTO>> PostBoMon(BoMonDTO boMonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _khoaRepository.ExistsAsync(boMonDTO.MaKhoa))
            {
                return BadRequest($"Không tồn tại khoa có mã: {boMonDTO.MaKhoa}");
            }

            var boMon = new BoMon
            {
                MaBoMon = boMonDTO.MaBoMon,
                TenBoMon = boMonDTO.TenBoMon,
                MaKhoa = boMonDTO.MaKhoa
            };

            if (await _boMonRepository.AddAsync(boMon))
            {
                return CreatedAtAction(nameof(GetBoMon), new { id = boMon.MaBoMon }, boMonDTO);
            }
            return BadRequest("Không thể thêm bộ môn.");
        }

        // PUT: api/BoMons/BM001
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoMon(string id, BoMonDTO boMonDTO)
        {
            if (id != boMonDTO.MaBoMon)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _boMonRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (!await _khoaRepository.ExistsAsync(boMonDTO.MaKhoa))
            {
                return BadRequest($"Không tồn tại khoa có mã: {boMonDTO.MaKhoa}");
            }

            var boMon = new BoMon
            {
                MaBoMon = boMonDTO.MaBoMon,
                TenBoMon = boMonDTO.TenBoMon,
                MaKhoa = boMonDTO.MaKhoa
            };

            if (await _boMonRepository.UpdateAsync(boMon))
            {
                return NoContent();
            }
            return BadRequest("Không thể cập nhật bộ môn.");
        }

        // DELETE: api/BoMons/BM001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoMon(string id)
        {
            if (!await _boMonRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            if (await _boMonRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Không thể xóa bộ môn.");
        }
    }
}