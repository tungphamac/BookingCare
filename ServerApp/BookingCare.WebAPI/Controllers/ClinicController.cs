using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
<<<<<<< HEAD
using BookingCare.Data.Models;
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> main
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;
<<<<<<< HEAD

        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        // GET: api/clinic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicDetailDto>>> GetClinics()
        {
            var clinics = await _clinicService.GetAllAsync();
            return Ok(clinics);
        }

        // GET: api/clinic/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDetailDto>> GetClinicDetail(int id)
        {
            var clinic = await _clinicService.GetClinicDetailAsync(id);
            if (clinic == null)
            {
                return NotFound(new { Message = $"Clinic with ID {id} not found." });
            }
            return Ok(clinic);
        }

        // POST: api/clinic
        [HttpPost]
        public async Task<ActionResult<ClinicDetailDto>> AddClinic([FromBody] ClinicDetailDto clinicDto)
        {
            if (clinicDto == null)
                return BadRequest("Clinic data is null");

            var clinic = new Clinic
            {
                Name = clinicDto.Name,
                Address = clinicDto.Address,
                Phone = clinicDto.Phone,
                Introduction = clinicDto.Introduction,
                CreateAt = DateTime.UtcNow
            };

            var result = await _clinicService.AddAsync(clinic);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetClinicDetail), new { id = clinic.Id }, clinicDto);
            }
            return StatusCode(500, "An error occurred while adding the clinic.");
        }

        // PUT: api/clinic/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClinic(int id, [FromBody] ClinicDetailDto clinicDto)
        {
            if (clinicDto == null || id != clinicDto.Id)
                return BadRequest("Invalid clinic data");

            var clinic = await _clinicService.GetByIdAsync(id);
            if (clinic == null)
                return NotFound($"Clinic with ID {id} not found.");

            clinic.Name = clinicDto.Name;
            clinic.Address = clinicDto.Address;
            clinic.Phone = clinicDto.Phone;
            clinic.Introduction = clinicDto.Introduction;

            var result = await _clinicService.UpdateAsync(clinic);
            if (result)
                return NoContent(); // Successfully updated

            return StatusCode(500, "An error occurred while updating the clinic.");
        }

        // DELETE: api/clinic/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            var clinic = await _clinicService.GetByIdAsync(id);
            if (clinic == null)
                return NotFound($"Clinic with ID {id} not found.");

            var result = await _clinicService.DeleteAsync(clinic);
            if (result)
                return NoContent(); // Successfully deleted

            return StatusCode(500, "An error occurred while deleting the clinic.");
        }
    }
}
=======
        private readonly ILogger<ClinicController> _logger;

        public ClinicController(IClinicService clinicService, ILogger<ClinicController> logger)
        {
            _clinicService = clinicService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Patient,Doctor")] // Admin, Patient, Doctor có thể xem chi tiết clinic
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

        [HttpGet]
        [Authorize(Roles = "Admin")] // Chỉ Admin được lấy danh sách clinic
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

        [HttpPost]
        [Authorize(Roles = "Admin")] // Chỉ Admin được tạo clinic
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Chỉ Admin được sửa clinic
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Chỉ Admin được xóa clinic
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
            catch(Exception ex)
            {
                return Problem($"Error get top clinics: {ex.Message}");
            }
        }
    }
}
>>>>>>> main
