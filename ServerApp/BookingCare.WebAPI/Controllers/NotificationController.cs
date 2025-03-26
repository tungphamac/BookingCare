using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications([FromQuery] int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest("Invalid user ID.");
                }

                var notifications = await _notificationService.GetNotificationsAsync(userId);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving notifications for UserId {userId}.");
                return StatusCode(500, "An error occurred while retrieving notifications.");
            }
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetAppointmentDetail(int appointmentId)
        {
            try
            {
                var appointment = await _notificationService.GetAppointmentDetailAsync(appointmentId);
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {appointmentId} not found.");
                }

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving appointment details for AppointmentId {appointmentId}.");
                return StatusCode(500, "An error occurred while retrieving appointment details.");
            }
        }

        [HttpPost("respond/{appointmentId}")]
        public async Task<IActionResult> RespondToAppointment(int appointmentId, [FromQuery] bool accept)
        {
            try
            {
                await _notificationService.RespondToAppointmentAsync(appointmentId, accept);
                return Ok(new { Message = $"Appointment {appointmentId} has been {(accept ? "accepted" : "rejected")}." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error responding to appointment {appointmentId}.");
                return StatusCode(500, "An error occurred while responding to the appointment.");
            }
        }
    }
}