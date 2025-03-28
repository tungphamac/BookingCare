﻿using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorService doctorService, ILogger<DoctorController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Patient,Doctor")] // Admin, Patient, Doctor có thể xem chi tiết bác sĩ
        public async Task<IActionResult> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorDetailAsync(id);
                if (doctor == null)
                {
                    return NotFound(new { Message = $"Doctor with ID {id} not found." });
                }
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving doctor with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the doctor.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Patient")] // Chỉ Admin, patient được lấy danh sách bác sĩ
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorService.GetAllDoctorsAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all doctors.");
                return StatusCode(500, "An error occurred while retrieving doctors.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Chỉ Admin được tạo bác sĩ
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto createDoctorDto)
        {
            try
            {
                var doctorId = await _doctorService.CreateDoctorAsync(createDoctorDto);
                return Ok(new { Message = "Doctor created successfully.", DoctorId = doctorId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new doctor.");
                return StatusCode(500, "An error occurred while creating the doctor.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Chỉ Admin được sửa bác sĩ
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] DoctorUpdateDto doctorUpdateDto)
        {
            try
            {
                var result = await _doctorService.UpdateDoctorAsync(id, doctorUpdateDto);
                if (!result)
                {
                    return NotFound(new { Message = $"Doctor with ID {id} not found." });
                }
                return Ok(new { Message = "Doctor updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating doctor with ID {id}.");
                return StatusCode(500, "An error occurred while updating the doctor.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Chỉ Admin được xóa bác sĩ
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                var result = await _doctorService.DeleteDoctorAsync(id);
                if (!result)
                {
                    return NotFound(new { Message = $"Doctor with ID {id} not found." });
                }
                return Ok(new { Message = "Doctor deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting doctor with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the doctor.");
            }
        }

        [HttpPost("lock/{id}")]
        [Authorize(Roles = "Admin")] // Chỉ Admin được khóa/mở khóa tài khoản bác sĩ
        public async Task<IActionResult> LockDoctorAccount(int id, [FromQuery] DateTime lockUntil)
        {
            try
            {
                var result = await _doctorService.LockUserAccountAsync(id, lockUntil);
                if (!result)
                {
                    return NotFound(new { Message = $"User with ID {id} not found." });
                }
                return Ok(new { Message = $"User account with ID {id} has been locked until {lockUntil}." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error locking account for user with ID {id}.");
                return StatusCode(500, "An error occurred while locking the account.");
            }
        }

        [HttpGet("get-top-doctors")]
        public async Task<IActionResult> GetTopDoctors()
        {
            try
            {
                var doctors = await _doctorService.GetFeaturedDoctors(5);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return Problem($"Error fetching top doctors: {ex.Message}");
            }
        }
    }
}