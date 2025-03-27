using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(IScheduleService scheduleService, ILogger<ScheduleController> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Doctor,Patient")] // Doctor, Patient có thể xem chi tiết lịch
        public async Task<IActionResult> GetScheduleById(int id)
        {
            try
            {
                var schedule = await _scheduleService.GetScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return NotFound(new { Message = $"Schedule with ID {id} not found." });
                }
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving schedule with ID {id}.");
                return StatusCode(500, "An error occurred while retrieving the schedule.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Doctor,Patient")] // Doctor, Patient có thể xem danh sách lịch
        public async Task<IActionResult> GetAllSchedules()
        {
            try
            {
                var schedules = await _scheduleService.GetAllSchedulesAsync();
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all schedules.");
                return StatusCode(500, "An error occurred while retrieving schedules.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")] // Chỉ Doctor được tạo lịch
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleDetailDto scheduleDto)
        {
            try
            {
                // Lấy DoctorId từ token
                var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var scheduleId = await _scheduleService.CreateScheduleAsync(scheduleDto, doctorId);
                return Ok(new { Message = "Schedule created successfully.", ScheduleId = scheduleId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new schedule.");
                return StatusCode(500, "An error occurred while creating the schedule.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")] // Chỉ Doctor được sửa lịch
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleDetailDto scheduleDto)
        {
            try
            {
                // Lấy DoctorId từ token
                var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var result = await _scheduleService.UpdateScheduleAsync(id, scheduleDto, doctorId);
                if (!result)
                {
                    return NotFound(new { Message = $"Schedule with ID {id} not found." });
                }
                return Ok(new { Message = "Schedule updated successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating schedule with ID {id}.");
                return StatusCode(500, "An error occurred while updating the schedule.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")] // Chỉ Doctor được xóa lịch
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                // Lấy DoctorId từ token
                var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var result = await _scheduleService.DeleteScheduleAsync(id, doctorId);
                if (!result)
                {
                    return NotFound(new { Message = $"Schedule with ID {id} not found." });
                }
                return Ok(new { Message = "Schedule deleted successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting schedule with ID {id}.");
                return StatusCode(500, "An error occurred while deleting the schedule.");
            }
        }
    }
}