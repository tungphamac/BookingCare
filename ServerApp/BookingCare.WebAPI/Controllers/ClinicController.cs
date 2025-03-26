using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;

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
