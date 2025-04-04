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
                return Ok(new { message = "Fetched all feedbacks successfully.", data = feedbacks });
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
                return Ok(new { message = "Feedback retrieved successfully.", data = feedback });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
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
                // Kiểm tra xem appointmentId đã có feedback chưa
                var existingFeedback = await _feedbackService.GetFeedbackByAppointmentAsync(feedbackVm.AppointmentId);
                if (existingFeedback != null)
                {
                    return BadRequest(new { message = "This appointment already has a feedback." });
                }

                // Nếu chưa có feedback, tiến hành thêm mới
                bool result = await _feedbackService.AddFeedbackAsync(feedbackVm);
                if (!result)
                {
                    return BadRequest(new { message = "Failed to add feedback." });
                }

                return Ok(new { message = "Feedback added successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
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
                return Ok(new { message = "Feedback updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
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
                return Ok(new { message = "Feedback deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
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