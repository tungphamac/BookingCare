using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class NotificationService : BaseService<Notification>, INotificationService
    {
        public NotificationService(ILogger<NotificationService> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork)
        {
        }

        public async Task CreateNotificationAsync(int userId, string message, int appointmentId)
        {
            try
            {
                var notification = new Notification
                {
                    UserId = userId,
                    Message = message,
                    AppointmentId = appointmentId,
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.NotificationRepository.AddAsync(notification);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Notification created for UserId {userId}: {message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating notification for UserId {userId}.");
                throw;
            }
        }

        public async Task<List<NotificationDto>> GetNotificationsAsync(int userId)
        {
            try
            {
                var notifications = await _unitOfWork.NotificationRepository
                    .GetQuery(n => n.UserId == userId)
                    .OrderByDescending(n => n.CreatedAt)
                    .Select(n => new NotificationDto
                    {
                        Id = n.Id,
                        Message = n.Message,
                        AppointmentId = n.AppointmentId,
                        IsRead = n.IsRead,
                        CreatedAt = n.CreatedAt
                    })
                    .ToListAsync();

                return notifications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving notifications for UserId {userId}.");
                throw;
            }
        }

        public async Task<AppointmentDetailDto> GetAppointmentDetailAsync(int appointmentId)
        {
            try
            {
                var appointment = await _unitOfWork.AppointmentRepository
                    .GetQuery(a => a.Id == appointmentId)
                    .Include(a => a.Doctor).ThenInclude(d => d.User)
                    .Include(a => a.Patient).ThenInclude(p => p.User)
                    .Include(a => a.Schedule)
                    .Select(a => new AppointmentDetailDto
                    {
                        Id = a.Id,
                        DoctorName = a.Doctor.User.UserName,
                        PatientName = a.Patient.User.UserName,
                        ScheduleTime = a.Date.Add(a.Time), // Kết hợp Date và Time
                        Status = a.Status.ToString(), // Chuyển enum thành string
                        CreatedAt = a.CreatedAt
                    })
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    _logger.LogWarning($"Appointment with ID {appointmentId} not found.");
                    return null;
                }

                return appointment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving appointment details for AppointmentId {appointmentId}.");
                throw;
            }
        }

        public async Task RespondToAppointmentAsync(int appointmentId, bool accept)
        {
            try
            {
                var appointment = await _unitOfWork.AppointmentRepository
                    .GetQuery(a => a.Id == appointmentId)
                    .Include(a => a.Doctor).ThenInclude(d => d.User)
                    .Include(a => a.Patient).ThenInclude(p => p.User)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    throw new ArgumentException($"Appointment with ID {appointmentId} not found.");
                }

                // Cập nhật trạng thái cuộc hẹn
                appointment.Status = accept ? AppointmentStatus.Confirmed : AppointmentStatus.Rejected;
                _unitOfWork.AppointmentRepository.Update(appointment);

                // Tạo thông báo cho bệnh nhân
                var message = $"Bác sĩ {appointment.Doctor.User.UserName} đã {(accept ? "đồng ý" : "từ chối")} lịch hẹn của bạn.";
                await CreateNotificationAsync(appointment.Patient.UserId, message, appointmentId);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Appointment {appointmentId} responded: {appointment.Status}. Notification sent to PatientId {appointment.Patient.UserId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error responding to appointment {appointmentId}.");
                throw;
            }
        }
    }
}