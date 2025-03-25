using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: api/doctor/{id}
        [HttpGet("{id}")]
        //[Authorize(Policy = "DoctorOrAdmin")] // Chỉ Doctor và Admin được truy cập
        public async Task<ActionResult<DoctorDetailDto>> GetDoctorDetail(int id)
        {
            var doctor = await _doctorService.GetDoctorDetailAsync(id);
            if (doctor == null)
            {
                return NotFound(new { Message = $"Doctor with ID {id} not found." });
            }
            return Ok(doctor);
        }
    }
}