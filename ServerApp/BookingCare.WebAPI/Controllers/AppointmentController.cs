using BookingCare.Business.Dtos;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingCare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDTO dto)
        {
            var appointment = new Appointment
            {
                Date = dto.Date,
                Time = dto.Time,
                Status = AppointmentStatus.Pending,
                Reason = dto.Reason,
                DoctorId = dto.DoctorId,
                PatientId = int.Parse(User.Identity.Name),
                ScheduleId = dto.ScheduleId,
                ClinicId = dto.ClinicId
            };

            var result = await _appointmentService.CreateAppointmentAsync(appointment);
            return Ok(new
            {
                Success = true,
                Message = "Thêm lịch làm thành công",
                Data = result
            });
        }

        [HttpPut("{id}/manage")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> ManageAppointment(int id, [FromQuery] AppointmentStatus status)
        {
            var userId = int.Parse(User.Identity.Name);
            var result = await _appointmentService.ManageAppointmentAsync(id, status, userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDTO dto)
        {
            var appointment = new Appointment
            {
                Id = id,
                Date = dto.Date,
                Time = dto.Time,
                Reason = dto.Reason,
                ScheduleId = dto.ScheduleId
            };
            var userId = int.Parse(User.Identity.Name);
            var result = await _appointmentService.UpdateAppointmentAsync(appointment, userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> GetAppointmentDetail(int id)
        {
            var userId = int.Parse(User.Identity.Name);
            var appointment = await _appointmentService.GetAppointmentDetailAsync(id, userId);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }
    }
}
