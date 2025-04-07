using BookingCare.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using BookingCare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookingCare.Business.ViewModels;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(IFeedbackService feedbackService, ILogger<FeedbackController> logger)
        {
            _feedbackService = feedbackService;
            _logger = logger;
        }

        [HttpGet("get-all-feedbacks")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = await _feedbackService.GetAllFeedbacksAsync();
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return Problem($"Error fetching feedbacks: {ex.Message}");
            }
        }

        [HttpGet("get-feedback-by-id/{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
                return Ok (feedback);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Error retrieving feedback: {ex.Message}");
            }
        }
        [HttpPost("add-feedback")]
        public async Task<IActionResult> AddFeedback([FromBody] FeedbackVm feedbackVm)
        {
            try
            {
                // Lấy userId từ Claims (giả sử đã xác thực JWT hoặc cookie auth)
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized("Invalid or missing user ID.");
                }

                // Gọi service để thêm feedback
                bool result = await _feedbackService.AddFeedbackAsync(feedbackVm, userId);
                if (!result)
                {
                    return BadRequest("Failed to add feedback.");
                }

                return Ok("Feedback added successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Only the patient of this appointment can submit feedback.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Error adding feedback: {ex.Message}");
            }
        }

        [HttpPut("update-feedback-by-id/{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] UpdateFeedbackVm feedbackVm)
        {
            try
            {
                bool result = await _feedbackService.UpdateFeedback(id, feedbackVm);
                return Ok("Feedback updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Error updating feedback: {ex.Message}");
            }
        }

        [HttpDelete("delete-feedback-by-id/{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            try
            {
                bool result = await _feedbackService.DeleteFeedbackAsync(id);
                return Ok("Feedback deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Error deleting feedback: {ex.Message}");
            }
        }

        [HttpGet("get-feedback-by-appointment/{appointmentId}")]
        //[Authorize(Roles = "Doctor,Patient,Admin")] // Bác sĩ, bệnh nhân, hoặc admin có thể xem phản hồi
        public async Task<IActionResult> GetFeedbackByAppointment(int appointmentId)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackByAppointmentAsync(appointmentId);
                if (feedback == null)
                {
                    return NotFound(new { Message = $"Feedback for Appointment ID {appointmentId} not found." });
                }
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving feedback for Appointment ID {appointmentId}.");
                return StatusCode(500, "An error occurred while retrieving the feedback.");
            }
        }

        [HttpGet("get-feedbacks-by-doctorId/{doctorId}")]
        public async Task<IActionResult> GetFeedbacksByDoctor(int doctorId)
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbacksByDoctor(doctorId);

                if (feedbacks == null || !feedbacks.Any())
                {
                    return NotFound(new { message = "Không có phản hồi nào cho bác sĩ này." });
                }

                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return Problem($"Lỗi khi lấy phản hồi: {ex.Message}");
            }
        }
    }
}