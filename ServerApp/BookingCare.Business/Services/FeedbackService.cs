using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BookingCare.Business.Services
{
    public class FeedbackService : BaseService<Feedback>, IFeedbackService
    {
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FeedbackService> _logger;

        public FeedbackService(ILogger<FeedbackService> logger, IUnitOfWork unitOfWork, INotificationService notificationService)
            : base(logger, unitOfWork)
        {
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task CreateFeedbackAsync(CreateFeedbackDto feedbackDto, int userId) // Thêm userId làm tham số
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

                // Kiểm tra xem userId có phải là bệnh nhân của cuộc hẹn không
                if (appointment.PatientId != userId)
                {
                    throw new UnauthorizedAccessException("Only the patient of this appointment can submit feedback.");
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

        public async Task<IEnumerable<FeedbackVm>> GetAllFeedbacksAsync()
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync();
            return feedbacks.Select(f => new FeedbackVm
            {
                AppointmentId = f.AppointmentId,
                Rating = f.Rating,
                Comment = f.Comment
            }).ToList();
        }

        public async Task<FeedbackVm> GetFeedbackByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid Feedback ID.");

            var feedback = await _unitOfWork.Context.Feedbacks.FindAsync(id);
            if (feedback == null) throw new ArgumentException("Feedback not found.");

            return new FeedbackVm()
            {
                AppointmentId = feedback.AppointmentId,
                Rating = feedback.Rating,
                Comment = feedback.Comment
            };
        }

        public async Task<bool> AddFeedbackAsync(FeedbackVm feedbackVm)
        {
            ValidateFeedback(feedbackVm);

            // Kiểm tra xem AppointmentId đã tồn tại trong bảng Feedback chưa
            bool exists = await _unitOfWork.Context.Feedbacks
                .AnyAsync(f => f.AppointmentId == feedbackVm.AppointmentId);

            if (exists)
            {
                throw new InvalidOperationException("Feedback for this appointment already exists.");
            }

            var feedback = new Feedback()
            {
                AppointmentId = feedbackVm.AppointmentId,
                Rating = feedbackVm.Rating,
                Comment = feedbackVm.Comment
            };

            await _unitOfWork.Context.Feedbacks.AddAsync(feedback);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateFeedback(int id, UpdateFeedbackVm updateFeedbackVm)
        {
            if (id <= 0) throw new ArgumentException("Invalid Feedback ID.");
            if (updateFeedbackVm.Rating < 1 || updateFeedbackVm.Rating > 5) throw new ArgumentException("Rating must be between 1 and 5.");
            if (string.IsNullOrWhiteSpace(updateFeedbackVm.Comment)) throw new ArgumentException("Comment cannot be empty.");

            using var transaction = await _unitOfWork.Context.Database.BeginTransactionAsync();
            try
            {
                var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
                if (feedback == null) throw new ArgumentException("Feedback not found.");

                feedback.Rating = updateFeedbackVm.Rating;
                feedback.Comment = updateFeedbackVm.Comment;

                _unitOfWork.FeedbackRepository.Update(feedback);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid Feedback ID.");

            var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
            if (feedback == null) throw new ArgumentException("Feedback not found.");

            await _unitOfWork.FeedbackRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private void ValidateFeedback(FeedbackVm feedbackVm)
        {
            if (feedbackVm == null) throw new ArgumentException("Feedback data cannot be null.");
            if (feedbackVm.AppointmentId <= 0) throw new ArgumentException("Invalid Appointment ID.");
            if (feedbackVm.Rating < 1 || feedbackVm.Rating > 5) throw new ArgumentException("Rating must be between 1 and 5.");
            if (string.IsNullOrWhiteSpace(feedbackVm.Comment)) throw new ArgumentException("Comment cannot be empty.");
        }
    }
}