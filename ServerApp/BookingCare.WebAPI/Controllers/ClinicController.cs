using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        private readonly ILogger<ClinicController> _logger;

        public ClinicController(IClinicService clinicService, ILogger<ClinicController> logger)
        {
            _clinicService = clinicService;
            _logger = logger;
        }

        [HttpGet("get-clinic-by-id/{id}")]
        //[Authorize(Roles = "Admin,Patient,Doctor")] // Admin, Patient, Doctor có thể xem chi tiết clinic

        public async Task<IActionResult> GetClinicById(int id)
        {
            try
            {
                var clinic = await _clinicService.GetClinicByIdAsync(id);
                return Ok(clinic);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving clinic with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the clinic.");
            }
        }


        [HttpGet("get-all-clinics")]
        
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllClinics()
        {
            try
            {
                var clinics = await _clinicService.GetAllClinicsAsync();
                return Ok(clinics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all clinics.");
                return StatusCode(500, "An error occurred while retrieving clinics.");
            }
        }

        [HttpPost("add")]
        //[Authorize(Roles = "Admin")] // Chỉ Admin được tạo clinic
       
        public async Task<IActionResult> CreateClinic([FromBody] ClinicDetailDto clinicDto)
        {
            try
            {
                await _clinicService.CreateClinicAsync(clinicDto);
                return Ok(new { Message = "Clinic created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating clinic {clinicDto.Name}.");
                return StatusCode(500, "An error occurred while creating the clinic.");
            }
        }

        [HttpPut("update/{id}")]
        //[Authorize(Roles = "Admin")] // Chỉ Admin được sửa clinic
        
        public async Task<IActionResult> UpdateClinic(int id, [FromBody] ClinicDetailDto clinicDto)
        {
            try
            {
                await _clinicService.UpdateClinicAsync(id, clinicDto);
                return Ok(new { Message = "Clinic updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating clinic with ID {id}.");
                return StatusCode(500, "An error occurred while updating the clinic.");
            }
        }

        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Admin")] // Chỉ Admin được xóa clinic
        
        public async Task<IActionResult> DeleteClinic(int id)
        {
            try
            {
                await _clinicService.DeleteClinicAsync(id);
                return Ok(new { Message = "Clinic deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting clinic with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the clinic.");
            }
        }

        [HttpGet("get-top-clinic")]
        public async Task<IActionResult> GetTopClinics()
        {
            try
            {
                var clinics = await _clinicService.GetTopClinics(3);
                return Ok(clinics);
            }
            catch (Exception ex)
            {
                return Problem($"Error get top clinics: {ex.Message}");
            }
        }

        [HttpGet("get-doctors-by-clinic-id/{clinicId}")]
        public async Task<IActionResult> GetDoctorsByClinicId(int clinicId)
        {
            try
            {
                var doctors = await _clinicService.GetDoctorsByClinicIdAsync(clinicId);
                return Ok(doctors);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving doctors for clinic ID {clinicId}.");
                return StatusCode(500, "An error occurred while retrieving doctors.");
            }
        }

        [HttpGet("get-clinics-by-specialization/{specializationId}")]
        public async Task<IActionResult> GetClinicsBySpecializationId(int specializationId)
        {
            try
            {
                var clinics = await _clinicService.GetClinicsBySpecializationIdAsync(specializationId);
                return Ok(clinics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving clinics for Specialization ID {specializationId}.");
                return StatusCode(500, "An error occurred while retrieving clinics.");
            }
        }
    }
}