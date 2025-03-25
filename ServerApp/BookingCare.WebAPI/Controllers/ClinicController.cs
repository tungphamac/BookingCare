using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
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
    }
}