using BookingCare.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using BookingCare.Business.Services.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackDto feedbackDto)
        {
            try
            {
                await _feedbackService.CreateFeedbackAsync(feedbackDto);
                return Ok(new { Message = "Feedback created successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating feedback for Appointment ID {feedbackDto.AppointmentId}.");
                return StatusCode(500, "An error occurred while creating the feedback.");
            }
        }

        [HttpGet("appointment/{appointmentId}")]
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
    }
}