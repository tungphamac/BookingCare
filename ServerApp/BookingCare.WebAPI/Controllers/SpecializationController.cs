<<<<<<< HEAD
﻿using BookingCare.Business.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.WebAPI.Controllers
=======
﻿using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
>>>>>>> main
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
<<<<<<< HEAD

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        // GET: api/specializations
        [HttpGet]
        public async Task<IActionResult> GetSpecializations()
        {
            var specializations = await _specializationService.GetAllAsync();
            return Ok(specializations);
        }

        // GET: api/specializations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialization(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound(new { Message = $"Specialization with ID {id} not found." });
            }
            return Ok(specialization);
        }

        // POST: api/specializations/create
        [HttpPost("create")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSpecialization([FromBody] SpecializationDto specializationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var specialization = new Specialization
            {
                Name = specializationDto.Name,
                Description = specializationDto.Description,
                Image = specializationDto.Image
            };

            var result = await _specializationService.AddAsync(specialization);
            if (result <= 0)
                return StatusCode(500, "An error occurred while creating the specialization.");

            return Ok(new { Message = "Specialization created successfully." });
        }

        // PUT: api/specializations/{id}/update
        [HttpPut("{id}/update")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSpecialization(int id, [FromBody] SpecializationDto specializationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSpecialization = await _specializationService.GetByIdAsync(id);
            if (existingSpecialization == null)
                return NotFound(new { Message = $"Specialization with ID {id} not found." });

            existingSpecialization.Name = specializationDto.Name;
            existingSpecialization.Description = specializationDto.Description;
            existingSpecialization.Image = specializationDto.Image;

            var result = await _specializationService.UpdateAsync(existingSpecialization);
            if (!result)
                return StatusCode(500, "An error occurred while updating the specialization.");

            return Ok(new { Message = "Specialization updated successfully." });
        }

        // DELETE: api/specializations/{id}/delete
        [HttpDelete("{id}/delete")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
                return NotFound(new { Message = $"Specialization with ID {id} not found." });

            var result = await _specializationService.DeleteAsync(specialization);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the specialization.");

            return Ok(new { Message = "Specialization deleted successfully." });
        }
    }

}
=======
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

        [HttpGet]
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
    }
}
>>>>>>> main
