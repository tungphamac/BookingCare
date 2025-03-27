using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IFeedbackService : IBaseService<Feedback>
    {
        Task<IEnumerable<FeedbackVm>> GetAllFeedbacksAsync();
        Task<FeedbackVm> GetFeedbackByIdAsync(int id);
        Task<bool> AddFeedbackAsync(FeedbackVm feedbackVm);

        Task<bool> UpdateFeedback(int id, UpdateFeedbackVm updateFeedbackVm);
        Task<bool> DeleteFeedbackAsync(int id);
        Task<FeedbackDetailDto> GetFeedbackByAppointmentAsync(int appointmentId);
    }
}