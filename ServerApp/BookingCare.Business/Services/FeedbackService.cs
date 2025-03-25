using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class FeedbackService : BaseService<Feedback>, IFeedbackService
    {
        private readonly INotificationService _notificationService;

        public FeedbackService(ILogger<FeedbackService> logger, IUnitOfWork unitOfWork, INotificationService notificationService)
            : base(logger, unitOfWork)
        {
            _notificationService = notificationService;
        }

        public async Task CreateFeedbackAsync(CreateFeedbackDto feedbackDto)
        {
            try
            {
                // Kiểm tra xem cuộc hẹn có tồn tại không
                var appointment = await _unitOfWork.AppointmentRepository
                    .GetQuery(a => a.Id == feedbackDto.AppointmentId)
                    .Include(a => a.Doctor).ThenInclude(d => d.User)
                    .Include(a => a.Patient).ThenInclude(p => p.User)
                    .FirstOrDefaultAsync();

                if (appointment == null)
                {
                    throw new ArgumentException($"Appointment with ID {feedbackDto.AppointmentId} not found.");
                }

                // Kiểm tra xem cuộc hẹn đã có phản hồi chưa
                var existingFeedback = await _unitOfWork.FeedbackRepository
                    .GetQuery(f => f.AppointmentId == feedbackDto.AppointmentId)
                    .FirstOrDefaultAsync();

                if (existingFeedback != null)
                {
                    throw new InvalidOperationException($"A feedback for Appointment ID {feedbackDto.AppointmentId} already exists.");
                }

                // Tạo phản hồi mới
                var feedback = new Feedback
                {
                    AppointmentId = feedbackDto.AppointmentId,
                    Rating = feedbackDto.Rating,
                    Comment = feedbackDto.Comment,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.FeedbackRepository.AddAsync(feedback);
                await _unitOfWork.SaveChangesAsync();

                // Tạo thông báo cho bác sĩ
                var message = $"Bệnh nhân {appointment.Patient.User.UserName} đã phản hồi về dịch vụ của bạn.";
                await _notificationService.CreateNotificationAsync(appointment.Doctor.UserId, message, appointment.Id);

                _logger.LogInformation($"Feedback created for Appointment ID {feedbackDto.AppointmentId}. Notification sent to Doctor ID {appointment.Doctor.UserId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating feedback for Appointment ID {feedbackDto.AppointmentId}.");
                throw;
            }
        }

        public async Task<FeedbackDetailDto> GetFeedbackByAppointmentAsync(int appointmentId)
        {
            try
            {
                var feedback = await _unitOfWork.FeedbackRepository
                    .GetQuery(f => f.AppointmentId == appointmentId)
                    .Select(f => new FeedbackDetailDto
                    {
                        Id = f.Id,
                        AppointmentId = f.AppointmentId,
                        Rating = f.Rating,
                        Comment = f.Comment,
                        CreatedAt = f.CreatedAt
                    })
                    .FirstOrDefaultAsync();

                if (feedback == null)
                {
                    _logger.LogWarning($"Feedback for Appointment ID {appointmentId} not found.");
                    return null;
                }

                return feedback;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving feedback for Appointment ID {appointmentId}.");
                throw;
            }
        }
    }
}