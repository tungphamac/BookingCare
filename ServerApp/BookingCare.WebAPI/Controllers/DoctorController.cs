using BookingCare.API.Dtos;
using BookingCare.Business.Services;
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
        private readonly IAccountService _accountService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto createDoctorDto)
        {
            try
            {
                var doctorId = await _doctorService.CreateDoctorAsync(createDoctorDto);
                return CreatedAtAction(nameof(GetDoctorDetail), new { id = doctorId }, new { Message = "Doctor created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the doctor." });
            }
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


        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctor(int doctorId, [FromBody] DoctorUpdateDto doctorUpdateDto)
        {
            try
            {
                var result = await _doctorService.UpdateDoctorAsync(doctorId, doctorUpdateDto);

                if (!result)
                {
                    return NotFound(new { Message = $"Doctor with UserId {doctorId} not found." });
                }

                return Ok(new { Message = "Doctor updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the doctor.", Details = ex.Message });
            }
        }

        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> DeleteDoctor(int doctorId)
        {
            try
            {
                var result = await _doctorService.DeleteDoctorAsync(doctorId);

                if (!result)
                {
                    return NotFound(new { Message = $"Doctor with UserId {doctorId} not found." });
                }

                return Ok(new { Message = "Doctor deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the doctor.", Details = ex.Message });
            }
        }

        



    }
}