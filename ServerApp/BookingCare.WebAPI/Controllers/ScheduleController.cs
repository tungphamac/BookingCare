using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("get-schedule-by-id/{id}")]
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

        [HttpPost("Create-schedule-by-doctor/{doctorId}")]
        public async Task<IActionResult> CreateSchedule(int doctorId, [FromBody] CreateScheduleDto scheduleDto)
        {
            try
            {
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

        [HttpPut("edit-schedule-by-id/{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] UpdateScheduleDto scheduleDto)
        {
            try
            {
                var result = await _scheduleService.UpdateScheduleAsync(id, scheduleDto); // Bỏ doctorId
                if (!result)
                {
                    return NotFound(new { Message = $"Schedule with ID {id} not found." });
                }
                return Ok(new { Message = "Schedule updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating schedule with ID {id}.");
                return StatusCode(500, new { Message = "An error occurred while updating the schedule.", Details = ex.Message });
            }
        }

        [HttpGet("get-schedules-by-id/{doctorId}")]
        public async Task<IActionResult> GetSchedulesByDoctorId(int doctorId)
        {
            try
            {
                var schedules = await _scheduleService.GetSchedulesByDoctorIdAsync(doctorId);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving schedules for Doctor ID {doctorId}.");
                return StatusCode(500, "An error occurred while retrieving schedules.");
            }
        }

        [HttpDelete("delete-schedule-by-id/{id}")] // Đổi sang HttpDelete cho phù hợp
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                var result = await _scheduleService.DeleteScheduleAsync(id); // Bỏ doctorId
                if (!result)
                {
                    return NotFound(new { Message = $"Schedule with ID {id} not found." });
                }
                return Ok(new { Message = "Schedule deleted successfully." });
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