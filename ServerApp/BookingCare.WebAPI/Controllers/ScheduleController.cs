using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingCare.API.Controllers
{
    [Authorize]
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

        private int GetLoggedInDoctorId()
        {
            var doctorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(doctorIdClaim) || !int.TryParse(doctorIdClaim, out var doctorId))
            {
                throw new UnauthorizedAccessException("Unable to retrieve Doctor ID from token.");
            }
            return doctorId;
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

        [HttpPost("Create-schedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleDto scheduleDto)
        {
            try
            {
                var doctorId = GetLoggedInDoctorId();
                var scheduleId = await _scheduleService.CreateScheduleAsync(scheduleDto, doctorId);
                return Ok(new { Message = "Schedule created successfully.", ScheduleId = scheduleId });
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
                _logger.LogError(ex, "Error creating new schedule.");
                return StatusCode(500, "An error occurred while creating the schedule.");
            }
        }

        [HttpPut("edit-schedule-by-id/{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] UpdateScheduleDto scheduleDto)
        {
            try
            {
                var doctorId = GetLoggedInDoctorId();
                var schedule = await _scheduleService.GetScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return NotFound(new { Message = $"Schedule with ID {id} not found." });
                }

                if (schedule.DoctorId != doctorId)
                {
                    return Forbid("You are not authorized to update this schedule.");
                }

                var result = await _scheduleService.UpdateScheduleAsync(id, scheduleDto);
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

        [HttpDelete("delete-schedule-by-id/{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                var doctorId = GetLoggedInDoctorId();
                var schedule = await _scheduleService.GetScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return NotFound(new { Message = $"Schedule with ID {id} not found." });
                }

                if (schedule.DoctorId != doctorId)
                {
                    return Forbid("You are not authorized to delete this schedule.");
                }

                var result = await _scheduleService.DeleteScheduleAsync(id);
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