using BookingCare.API.Dtos;
using BookingCare.Business.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDto dto)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (dto == null || !ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid appointment data provided.");
                    return BadRequest(new { Success = false, Message = "Invalid appointment data." });
                }

                // Lấy PatientId từ token
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var patientId))
                {
                    _logger.LogWarning("Invalid user ID in token.");
                    return Unauthorized(new { Success = false, Message = "Invalid user ID." });
                }

                var appointment = new Appointment
                {
                    Date = dto.Date,
                    Time = dto.Time,
                    Status = AppointmentStatus.Pending,
                    Reason = dto.Reason,
                    DoctorId = dto.DoctorId,
                    PatientId = patientId,
                    ScheduleId = dto.ScheduleId,
                    ClinicId = dto.ClinicId,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _appointmentService.CreateAppointmentAsync(appointment);
                _logger.LogInformation($"Appointment {result} created successfully by Patient ID {patientId}.");

                return Ok(new
                {
                    Success = true,
                    Message = "Thêm lịch hẹn thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new appointment.");
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}/manage")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> ManageAppointment(int id, [FromQuery] AppointmentStatus status)
        {
            try
            {
                // Kiểm tra trạng thái hợp lệ
                if (!Enum.IsDefined(typeof(AppointmentStatus), status))
                {
                    _logger.LogWarning($"Invalid appointment status: {status}.");
                    return BadRequest(new { Success = false, Message = "Invalid appointment status." });
                }

                // Lấy userId từ token
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    _logger.LogWarning("Invalid user ID in token.");
                    return Unauthorized(new { Success = false, Message = "Invalid user ID." });
                }

                var result = await _appointmentService.ManageAppointmentAsync(id, status, userId);
                if (!result)
                {
                    _logger.LogWarning($"Appointment with ID {id} not found.");
                    return NotFound(new { Success = false, Message = $"Appointment with ID {id} not found." });
                }

                _logger.LogInformation($"Appointment {id} status updated to {status} by User ID {userId}.");
                return Ok(new { Success = true, Message = "Cập nhật trạng thái lịch hẹn thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error managing appointment with ID {id}.");
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentCreateDto dto)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (dto == null || !ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid appointment data provided for update.");
                    return BadRequest(new { Success = false, Message = "Invalid appointment data." });
                }

                // Lấy userId từ token
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    _logger.LogWarning("Invalid user ID in token.");
                    return Unauthorized(new { Success = false, Message = "Invalid user ID." });
                }

                var appointment = new Appointment
                {
                    Id = id,
                    Date = dto.Date,
                    Time = dto.Time,
                    Reason = dto.Reason,
                    ScheduleId = dto.ScheduleId
                };

                var result = await _appointmentService.UpdateAppointmentAsync(appointment, userId);
                if (!result)
                {
                    _logger.LogWarning($"Appointment with ID {id} not found or user {userId} is not authorized.");
                    return NotFound(new { Success = false, Message = $"Appointment with ID {id} not found or you are not authorized to update it." });
                }

                _logger.LogInformation($"Appointment {id} updated successfully by Patient ID {userId}.");
                return Ok(new { Success = true, Message = "Cập nhật lịch hẹn thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating appointment with ID {id}.");
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> GetAppointmentDetail(int id)
        {
            try
            {
                // Lấy userId từ token
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    _logger.LogWarning("Invalid user ID in token.");
                    return Unauthorized(new { Success = false, Message = "Invalid user ID." });
                }

                var appointment = await _appointmentService.GetAppointmentDetailAsync(id, userId);
                if (appointment == null)
                {
                    _logger.LogWarning($"Appointment with ID {id} not found.");
                    return NotFound(new { Success = false, Message = $"Appointment with ID {id} not found." });
                }

                // Ánh xạ sang DTO để kiểm soát dữ liệu trả về
                var appointmentDto = new AppointmentDetailDto
                {
                    Id = appointment.Id,
                    DoctorName = appointment.Doctor?.User?.UserName ?? "Unknown",
                    PatientName = appointment.Patient?.User?.UserName ?? "Unknown",
                    ScheduleTime = appointment.Date.Add(appointment.Time),
                    Status = appointment.Status.ToString(),
                    CreatedAt = appointment.CreatedAt
                };

                return Ok(new { Success = true, Message = "Lấy chi tiết lịch hẹn thành công", Data = appointmentDto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving appointment with ID {id}.");
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }
    }
}