using Microsoft.AspNetCore.Mvc;
using QLDoAnAPI.DTOs;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/dotdoans")]
    public class DotDoAnController : ControllerBase
    {
        private readonly IDotDoAnRepository _repository;

        public DotDoAnController(IDotDoAnRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDotDoAns()
        {
            var dotDoAns = await _repository.GetAllAsync();
            var dotDoAnsDto = dotDoAns.Select(d => new DotDoAnDTO
            {
                MaDotDoAn = d.MaDotDoAn,
                TenDotDoAn = d.TenDotDoAn,
                KhoaHoc = d.KhoaHoc,
                NgayBatDau = d.NgayBatDau,
                NgayKetThuc = d.NgayKetThuc,
                SoTuanThucHien = d.SoTuanThucHien
            }).ToList();
            return Ok(dotDoAnsDto);
        }

        [HttpGet("{maDotDoAn}", Name = "GetDotDoAnById")]
        public async Task<IActionResult> GetDotDoAnById(string maDotDoAn)
        {
            var dotDoAn = await _repository.GetByIdAsync(maDotDoAn);

            if (dotDoAn == null)
            {
                return NotFound();
            }

            var dotDoAnDto = new DotDoAnDTO
            {
                MaDotDoAn = dotDoAn.MaDotDoAn,
                TenDotDoAn = dotDoAn.TenDotDoAn,
                KhoaHoc = dotDoAn.KhoaHoc,
                NgayBatDau = dotDoAn.NgayBatDau,
                NgayKetThuc = dotDoAn.NgayKetThuc,
                SoTuanThucHien = dotDoAn.SoTuanThucHien
            };
            return Ok(dotDoAnDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDotDoAn([FromBody] DotDoAnRequestDTO dotDoAnRequestDto)
        {
            if (dotDoAnRequestDto == null)
            {
                return BadRequest("Đối tượng DotDoAnRequestDTO là null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _repository.DotDoAnExistsAsync(dotDoAnRequestDto.MaDotDoAn))
            {
                ModelState.AddModelError(nameof(DotDoAnRequestDTO.MaDotDoAn), "Mã đợt đồ án đã tồn tại.");
                return Conflict(ModelState);
            }

            var dotDoAn = new DotDoAn
            {
                MaDotDoAn = dotDoAnRequestDto.MaDotDoAn,
                TenDotDoAn = dotDoAnRequestDto.TenDotDoAn,
                KhoaHoc = dotDoAnRequestDto.KhoaHoc,
                NgayBatDau = dotDoAnRequestDto.NgayBatDau,
                NgayKetThuc = dotDoAnRequestDto.NgayKetThuc,
                SoTuanThucHien = dotDoAnRequestDto.SoTuanThucHien
            };

            _repository.Create(dotDoAn);
            await _repository.SaveChangesAsync();

            var dotDoAnDto = new DotDoAnDTO
            {
                MaDotDoAn = dotDoAn.MaDotDoAn,
                TenDotDoAn = dotDoAn.TenDotDoAn,
                KhoaHoc = dotDoAn.KhoaHoc,
                NgayBatDau = dotDoAn.NgayBatDau,
                NgayKetThuc = dotDoAn.NgayKetThuc,
                SoTuanThucHien = dotDoAn.SoTuanThucHien
            };

            return CreatedAtRoute("GetDotDoAnById", new { maDotDoAn = dotDoAnDto.MaDotDoAn }, dotDoAnDto);
        }

        [HttpPut("{maDotDoAn}")]
        public async Task<IActionResult> UpdateDotDoAn(string maDotDoAn, [FromBody] DotDoAnRequestDTO dotDoAnRequestDto)
        {
            if (dotDoAnRequestDto == null)
            {
                return BadRequest("Đối tượng DotDoAnRequestDTO là null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dotDoAnFromRepo = await _repository.GetByIdAsync(maDotDoAn);

            if (dotDoAnFromRepo == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính
            dotDoAnFromRepo.TenDotDoAn = dotDoAnRequestDto.TenDotDoAn;
            dotDoAnFromRepo.KhoaHoc = dotDoAnRequestDto.KhoaHoc;
            dotDoAnFromRepo.NgayBatDau = dotDoAnRequestDto.NgayBatDau;
            dotDoAnFromRepo.NgayKetThuc = dotDoAnRequestDto.NgayKetThuc;
            dotDoAnFromRepo.SoTuanThucHien = dotDoAnRequestDto.SoTuanThucHien;

            _repository.Update(dotDoAnFromRepo);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{maDotDoAn}")]
        public async Task<IActionResult> DeleteDotDoAn(string maDotDoAn)
        {
            var dotDoAnFromRepo = await _repository.GetByIdAsync(maDotDoAn);

            if (dotDoAnFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(dotDoAnFromRepo);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}