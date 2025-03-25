using BookingCare.Business.Services;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.WebAPI.Properties
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return Ok(await _doctorService.GetAllDoctorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult> PostDoctor([FromBody] Doctor doctor)
        {
            await _doctorService.AddDoctorAsync(doctor);
            return CreatedAtAction("GetDoctor", new { id = doctor.UserId }, doctor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutDoctor(int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.UserId)
            {
                return BadRequest();
            }

            await _doctorService.UpdateDoctorAsync(doctor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return NoContent();
        }
    }

}
