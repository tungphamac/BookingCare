using BookingCare.Business.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;

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
