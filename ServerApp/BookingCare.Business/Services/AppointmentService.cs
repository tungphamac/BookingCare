using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        public AppointmentService(ILogger<BaseService<Appointment>> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork) { }

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

            return await AddAsync(appointment);
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
