using BookingCare.Business.Services;
using BookingCare.Data.DTOs;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly SpecializationService _specializationService;

        public SpecializationController(SpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        // GET: api/Specialization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> GetAllSpecializations()
        {
            var specializations = await _specializationService.GetAllSpecializationsAsync();
            var specializationDTOs = specializations.Select(s => new SpecializationDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Image = s.Image
            }).ToList();

            return Ok(specializationDTOs);
        }

        // GET: api/Specialization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecializationDTO>> GetSpecializationById(int id)
        {
            var specialization = await _specializationService.GetSpecializationByIdAsync(id);

            if (specialization == null)
            {
                return NotFound();
            }

            var specializationDTO = new SpecializationDTO
            {
                Id = specialization.Id,
                Name = specialization.Name,
                Description = specialization.Description,
                Image = specialization.Image 
            };

            return Ok(specializationDTO);
        }

        // POST: api/Specialization
        [HttpPost]
        public async Task<ActionResult<SpecializationDTO>> CreateSpecialization([FromBody] SpecializationDTO specializationDTO)
        {
            if (specializationDTO == null)
            {
                return BadRequest("SpecializationDTO is required.");
            }

            var specialization = new Specialization
            {
                Name = specializationDTO.Name,
                Description = specializationDTO.Description,
                Image = specializationDTO.Image
            };

            await _specializationService.AddSpecializationAsync(specialization);

            return CreatedAtAction(nameof(GetSpecializationById), new { id = specialization.Id }, specializationDTO);
        }

        // PUT: api/Specialization/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpecialization(int id, [FromBody] SpecializationDTO specializationDTO)
        {
            if (id != specializationDTO.Id)
            {
                return BadRequest("Specialization ID mismatch");
            }

            var existingSpecialization = await _specializationService.GetSpecializationByIdAsync(id);
            if (existingSpecialization == null)
            {
                return NotFound();
            }

            existingSpecialization.Name = specializationDTO.Name;
            existingSpecialization.Description = specializationDTO.Description;

            await _specializationService.UpdateSpecializationAsync(existingSpecialization);

            return NoContent();
        }

        // DELETE: api/Specialization/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var specialization = await _specializationService.GetSpecializationByIdAsync(id);

            if (specialization == null)
            {
                return NotFound();
            }

            await _specializationService.DeleteSpecializationAsync(id);

            return NoContent();
        }
    }

}
