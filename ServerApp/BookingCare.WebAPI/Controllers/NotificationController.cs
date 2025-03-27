using BookingCare.API.Dtos;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
>>>>>>> main

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
<<<<<<< HEAD
=======
        [Authorize(Roles = "Doctor,Patient,Admin")]
>>>>>>> main
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
<<<<<<< HEAD
=======
        [Authorize(Roles = "Doctor,Patient,Admin")]
>>>>>>> main
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
<<<<<<< HEAD
=======
        [Authorize(Roles = "Doctor")] // Chỉ bác sĩ mới được phản hồi lịch hẹn
>>>>>>> main
        public async Task<IActionResult> RespondToAppointment(int appointmentId, [FromQuery] bool accept)
        {
            try
            {
<<<<<<< HEAD
                await _notificationService.RespondToAppointmentAsync(appointmentId, accept);
=======
                // Lấy userId từ token
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId <= 0)
                {
                    return Unauthorized(new { Message = "Invalid user ID." });
                }

                await _notificationService.RespondToAppointmentAsync(appointmentId, accept, userId);
>>>>>>> main
                return Ok(new { Message = $"Appointment {appointmentId} has been {(accept ? "accepted" : "rejected")}." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
<<<<<<< HEAD
=======
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
>>>>>>> main
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error responding to appointment {appointmentId}.");
                return StatusCode(500, "An error occurred while responding to the appointment.");
            }
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> main
