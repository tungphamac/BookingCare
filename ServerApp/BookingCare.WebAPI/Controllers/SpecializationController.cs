using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
        private readonly ILogger<SpecializationController> _logger;

        public SpecializationController(ISpecializationService specializationService, ILogger<SpecializationController> logger)
        {
            _specializationService = specializationService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Patient")] // Admin, Patient có thể xem chi tiết specialization
        public async Task<IActionResult> GetSpecializationById(int id)
        {
            try
            {
                var specialization = await _specializationService.GetSpecializationByIdAsync(id);
                if (specialization == null)
                {
                    return NotFound(new { Message = $"Specialization with ID {id} not found." });
                }
                return Ok(specialization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving specialization with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the specialization.");
            }
        }

<<<<<<< HEAD
        [HttpGet]
=======
        [HttpGet("get-all-specializations")]
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
        [Authorize(Roles = "Admin,Patient")] // Admin, Patient có thể xem danh sách specialization
        public async Task<IActionResult> GetAllSpecializations()
        {
            try
            {
                var specializations = await _specializationService.GetAllSpecializationsAsync();
                return Ok(specializations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all specializations.");
                return StatusCode(500, "An error occurred while retrieving specializations.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Chỉ Admin được tạo specialization
        public async Task<IActionResult> CreateSpecialization([FromBody] SpecializationDetailDto specializationDto)
        {
            try
            {
                var specializationId = await _specializationService.CreateSpecializationAsync(specializationDto);
                return Ok(new { Message = "Specialization created successfully.", SpecializationId = specializationId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating specialization {specializationDto.Name}.");
                return StatusCode(500, "An error occurred while creating the specialization.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Chỉ Admin được sửa specialization
        public async Task<IActionResult> UpdateSpecialization(int id, [FromBody] SpecializationDetailDto specializationDto)
        {
            try
            {
                var result = await _specializationService.UpdateSpecializationAsync(id, specializationDto);
                if (!result)
                {
                    return NotFound(new { Message = $"Specialization with ID {id} not found." });
                }
                return Ok(new { Message = "Specialization updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating specialization with ID {id}.");
                return StatusCode(500, "An error occurred while updating the specialization.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Chỉ Admin được xóa specialization
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            try
            {
                var result = await _specializationService.DeleteSpecializationAsync(id);
                if (!result)
                {
                    return NotFound(new { Message = $"Specialization with ID {id} not found." });
                }
                return Ok(new { Message = "Specialization deleted successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting specialization with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the specialization.");
            }
        }
<<<<<<< HEAD
=======

        [HttpGet("get-top-specializations")]
        public async Task<IActionResult> GetTopSpecializations()
        {
            try
            {
                var topSpecializations = await _specializationService.GetTopSpecializationsAsync(3);
                return Ok(topSpecializations);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all specializations.");
                return StatusCode(500, "An error occurred while retrieving specializations.");
            }
        }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
    }
}