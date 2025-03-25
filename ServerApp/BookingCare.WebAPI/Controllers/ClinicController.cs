using BookingCare.Business.Services;
using BookingCare.Data.DTOs;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly ClinicService _clinicService;

        public ClinicController(ClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        // API GET để lấy tất cả phòng khám với tên bác sĩ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetClinics()
        {
            var clinics = await _clinicService.GetClinicsWithDoctorsAsync();
            return Ok(clinics);
        }

        // API GET để lấy phòng khám theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorCreateRequest>> GetClinic(int id)
        {
            var clinic = await _clinicService.GetClinicByIdAsync(id);
            if (clinic == null)
            {
                return NotFound("Clinic not found.");
            }
            return Ok(clinic);
        }

        // API POST để thêm phòng khám
        [HttpPost]
        public async Task<IActionResult> AddClinic([FromBody] ClinicCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                var clinic = new Clinic
                {
                    Name = request.Name,
                    Address = request.Address,
                    Phone = request.Phone,
                    Introduction = request.Introduction,
                    CreateAt = DateTime.UtcNow // Đặt thời gian tạo phòng khám
                };

                // Thêm phòng khám và danh sách bác sĩ
                await _clinicService.AddClinicAsync(clinic, request.DoctorIds);
                return Ok("Clinic added successfully.");
            }
            return BadRequest("Invalid data.");
        }

        // API PUT để cập nhật phòng khám
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClinic(int id, [FromBody] ClinicUpdateRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("Clinic ID mismatch.");
            }

            try
            {
                var clinic = new Clinic
                {
                    Id = request.Id,
                    Name = request.Name,
                    Address = request.Address,
                    Introduction = request.Introduction,
                    Phone = request.Phone,
                    CreateAt = DateTime.UtcNow // Cập nhật thời gian tạo phòng khám
                };

                // Cập nhật phòng khám và danh sách bác sĩ
                await _clinicService.UpdateClinicAsync(clinic, request.DoctorIds);
                return NoContent(); // Trả về 204 khi cập nhật thành công
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating clinic: {ex.Message}");
            }
        }
        // API DELETE để xóa phòng khám
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            try
            {
                await _clinicService.DeleteClinicAsync(id);
                return NoContent(); // Trả về 204 khi xóa thành công
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting clinic: {ex.Message}");
            }
        }
    }


}
