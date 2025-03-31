using BookingCare.API.Dtos;
using BookingCare.Business.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace BookingCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(
            IAppointmentService appointmentService,
            IUnitOfWork unitOfWork,
            ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _unitOfWork = unitOfWork;

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
                    _logger.LogWarning("Invalid appointment data provided: {Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return BadRequest(new { Success = false, Message = "Invalid appointment data.", Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

                }

                // Lấy PatientId từ token
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var patientId))
                {
                    _logger.LogWarning("Invalid user ID in token for creating appointment.");
                    return Unauthorized(new { Success = false, Message = "Invalid user ID in token." });

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
                _logger.LogInformation("Appointment {AppointmentId} created successfully by Patient ID {PatientId}.", result, patientId);


                return Ok(new
                {
                    Success = true,
                    Message = "Thêm lịch hẹn thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new appointment for Patient ID {PatientId}.", User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi tạo lịch hẹn: " + ex.Message });

            }
        }

        [HttpPut("{id}/manage")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> ManageAppointment(int id, [FromQuery] AppointmentStatus status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)

        {
            try
            {
                // Kiểm tra trạng thái hợp lệ
                if (!Enum.IsDefined(typeof(AppointmentStatus), status))
                {
                    _logger.LogWarning("Invalid appointment status provided: {Status}.", status);
                    return BadRequest(new { Success = false, Message = $"Trạng thái lịch hẹn không hợp lệ: {status}." });

                }

                // Lấy userId từ token
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    _logger.LogWarning("Invalid user ID in token for managing appointment ID {AppointmentId}.", id);
                    return Unauthorized(new { Success = false, Message = "Invalid user ID in token." });
                }

                // Lấy role từ token
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (string.IsNullOrEmpty(role) || (role != "Doctor" && role != "Patient"))
                {
                    _logger.LogWarning("Invalid role in token for managing appointment ID {AppointmentId}.", id);
                    return Unauthorized(new { Success = false, Message = "Invalid role in token." });
                }

                // Thay đổi trạng thái lịch hẹn
                var result = await _appointmentService.ManageAppointmentAsync(id, status, userId);
                if (!result)
                {
                    _logger.LogWarning("Appointment with ID {AppointmentId} not found or user {UserId} is not authorized to manage it.", id, userId);
                    return NotFound(new { Success = false, Message = $"Không tìm thấy lịch hẹn với ID {id} hoặc bạn không có quyền quản lý lịch hẹn này." });
                }

                // Lấy danh sách lịch hẹn sau khi thay đổi trạng thái
                var appointments = await _appointmentService.GetAppointmentsAsync(userId, role, pageNumber, pageSize);

                // Ánh xạ danh sách lịch hẹn sang DTO để trả về
                var appointmentDtos = appointments.Select(a => new AppointmentDetailDto
                {
                    Id = a.Id,
                    DoctorName = a.Doctor?.User?.UserName ?? "Không xác định",
                    PatientName = a.Patient?.User?.UserName ?? "Không xác định",
                    ScheduleTime = a.Date.Add(a.Time),
                    Status = a.Status.ToString(),
                    CreatedAt = a.CreatedAt,
                    Reason = a.Reason,
                    ClinicId = a.ClinicId
                }).ToList();

                _logger.LogInformation("Appointment {AppointmentId} status updated to {Status} by User ID {UserId}.", id, status, userId);
                return Ok(new
                {
                    Success = true,
                    Message = "Cập nhật trạng thái lịch hẹn thành công",
                    Data = appointmentDtos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error managing appointment with ID {AppointmentId} by User ID {UserId}.", id, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi quản lý lịch hẹn: " + ex.Message });

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
                    _logger.LogWarning("Invalid appointment data provided for update: {Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return BadRequest(new { Success = false, Message = "Invalid appointment data.", Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

                }

                // Lấy userId từ token
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    _logger.LogWarning("Invalid user ID in token for updating appointment ID {AppointmentId}.", id);
                    return Unauthorized(new { Success = false, Message = "Invalid user ID in token." });

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
                    _logger.LogWarning("Appointment with ID {AppointmentId} not found or user {UserId} is not authorized to update it.", id, userId);
                    return NotFound(new { Success = false, Message = $"Không tìm thấy lịch hẹn với ID {id} hoặc bạn không có quyền cập nhật lịch hẹn này." });
                }

                _logger.LogInformation("Appointment {AppointmentId} updated successfully by Patient ID {PatientId}.", id, userId);

                return Ok(new { Success = true, Message = "Cập nhật lịch hẹn thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating appointment with ID {AppointmentId} by Patient ID {PatientId}.", id, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi cập nhật lịch hẹn: " + ex.Message });

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
                    _logger.LogWarning("Invalid user ID in token for retrieving appointment ID {AppointmentId}.", id);
                    return Unauthorized(new { Success = false, Message = "Invalid user ID in token." });

                }

                var appointment = await _appointmentService.GetAppointmentDetailAsync(id, userId);
                if (appointment == null)
                {
                    _logger.LogWarning("Appointment with ID {AppointmentId} not found for User ID {UserId}.", id, userId);
                    return NotFound(new { Success = false, Message = $"Không tìm thấy lịch hẹn với ID {id}." });
                }

                // Truy vấn thông tin bác sĩ và bệnh nhân
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == appointment.DoctorId)
                    .Include(d => d.User)
                    .FirstOrDefaultAsync();

                var patient = await _unitOfWork.PatientRepository
                    .GetQuery(p => p.UserId == appointment.PatientId)
                    .Include(p => p.User)
                    .FirstOrDefaultAsync();

                // Ánh xạ sang DTO
                var appointmentDto = new AppointmentDetailDto
                {
                    Id = appointment.Id,
                    DoctorName = doctor?.User?.UserName ?? "Không xác định",
                    PatientName = patient?.User?.UserName ?? "Không xác định",
                    ScheduleTime = appointment.Date.Add(appointment.Time),
                    Status = appointment.Status.ToString(),
                    CreatedAt = appointment.CreatedAt,
                    Reason = appointment.Reason,
                    ClinicId = appointment.ClinicId,
                };

                _logger.LogInformation("Retrieved appointment details for ID {AppointmentId} by User ID {UserId}.", id, userId);

                return Ok(new { Success = true, Message = "Lấy chi tiết lịch hẹn thành công", Data = appointmentDto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment with ID {AppointmentId} by User ID {UserId}.", id, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return StatusCode(500, new { Success = false, Message = "Đã xảy ra lỗi khi lấy chi tiết lịch hẹn: " + ex.Message });

            }
        }
    }
}