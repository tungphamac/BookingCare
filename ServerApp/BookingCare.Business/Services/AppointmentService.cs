using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        private readonly INotificationService _notificationService;

        public AppointmentService(
            ILogger<BaseService<Appointment>> logger,
            IUnitOfWork unitOfWork,
            INotificationService notificationService)
            : base(logger, unitOfWork)
        {
            _notificationService = notificationService;
        }

        public async Task<int> CreateAppointmentAsync(Appointment appointment)
        {
            // Validate schedule availability
            var schedule = await _unitOfWork.ScheduleRepository.GetByIdAsync(appointment.ScheduleId);
            if (schedule == null || schedule.Status != ScheduleStatus.Available)
            {
                throw new Exception("Schedule is not available");
            }

            schedule.Status = ScheduleStatus.Booked;
            _unitOfWork.ScheduleRepository.Update(schedule);

            // Thêm Appointment
            await AddAsync(appointment);

            // Tạo thông báo cho bác sĩ
            var patient = await _unitOfWork.PatientRepository
                .GetQuery(p => p.UserId == appointment.PatientId)
                .Include(p => p.User)
                .FirstOrDefaultAsync();

            if (patient != null)
            {
                var message = $"Bệnh nhân {patient.User.UserName} đã đặt lịch hẹn vào ngày {appointment.Date:dd/MM/yyyy} lúc {appointment.Time}.";
                await _notificationService.CreateNotificationAsync(schedule.DoctorId, message, appointment.Id);
            }

            // Tạo thông báo cho bệnh nhân
            var doctor = await _unitOfWork.DoctorRepository
                .GetQuery(d => d.UserId == schedule.DoctorId)
                .Include(d => d.User)
                .FirstOrDefaultAsync();

            if (doctor != null)
            {
                var message = $"Bạn đã đặt lịch hẹn với bác sĩ {doctor.User.UserName} vào ngày {appointment.Date:dd/MM/yyyy} lúc {appointment.Time}.";
                await _notificationService.CreateNotificationAsync(appointment.PatientId, message, appointment.Id);
            }

            return appointment.Id;
        }

        public async Task<Appointment?> GetAppointmentDetailAsync(int appointmentId, int userId)
        {
            var appointment = await GetByIdAsync(appointmentId);
            if (appointment == null) return null;

            // Check if user has permission (admin, doctor, or patient)
            var isAdmin = (await _unitOfWork.UserRepository.GetByIdAsync(userId))?.Doctor == null &&
                         (await _unitOfWork.UserRepository.GetByIdAsync(userId))?.Patient == null;
            if (!isAdmin && appointment.DoctorId != userId && appointment.PatientId != userId)
                throw new Exception("Unauthorized access to appointment details");

            return appointment;
        }

        public async Task<bool> ManageAppointmentAsync(int appointmentId, AppointmentStatus status, int userId)
        {
            var appointment = await GetByIdAsync(appointmentId);
            if (appointment == null) return false;

            // Check if user is doctor or patient
            if (status == AppointmentStatus.Rejected && appointment.PatientId != userId)
                throw new Exception("Only patient can cancel appointment");
            if ((status == AppointmentStatus.Confirmed || status == AppointmentStatus.Rejected) && appointment.DoctorId != userId)
                throw new Exception("Only doctor can accept/decline appointment");

            appointment.Status = status;
            if (status == AppointmentStatus.Rejected)
            {
                var schedule = await _unitOfWork.ScheduleRepository.GetByIdAsync(appointment.ScheduleId);
                schedule.Status = ScheduleStatus.Available;
                _unitOfWork.ScheduleRepository.Update(schedule);
            }

            // Tạo thông báo cho bên còn lại
            if (status != AppointmentStatus.Pending) // Chỉ tạo thông báo khi trạng thái thay đổi (Confirmed hoặc Rejected)
            {
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == appointment.DoctorId)
                    .Include(d => d.User)
                    .FirstOrDefaultAsync();

                var patient = await _unitOfWork.PatientRepository
                    .GetQuery(p => p.UserId == appointment.PatientId)
                    .Include(p => p.User)
                    .FirstOrDefaultAsync();

                if (doctor != null && patient != null)
                {
                    if (userId == appointment.DoctorId) // Bác sĩ thực hiện hành động
                    {
                        var message = $"Bác sĩ {doctor.User.UserName} đã {(status == AppointmentStatus.Confirmed ? "đồng ý" : "từ chối")} lịch hẹn của bạn vào ngày {appointment.Date:dd/MM/yyyy} lúc {appointment.Time}.";
                        await _notificationService.CreateNotificationAsync(appointment.PatientId, message, appointmentId);
                    }
                    else if (userId == appointment.PatientId) // Bệnh nhân thực hiện hành động (hủy)
                    {
                        var message = $"Bệnh nhân {patient.User.UserName} đã hủy lịch hẹn vào ngày {appointment.Date:dd/MM/yyyy} lúc {appointment.Time}.";
                        await _notificationService.CreateNotificationAsync(appointment.DoctorId, message, appointmentId);
                    }
                }
            }

            return await UpdateAsync(appointment);
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment, int patientId)
        {
            var existing = await GetByIdAsync(appointment.Id);
            if (existing == null || existing.PatientId != patientId)
                return false;

            existing.Date = appointment.Date;
            existing.Time = appointment.Time;
            existing.Reason = appointment.Reason;
            existing.ScheduleId = appointment.ScheduleId;

            return await UpdateAsync(existing);
        }
    }
}