using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
<<<<<<< HEAD
=======
using BookingCare.Business.ViewModels;
>>>>>>> main
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IFeedbackService : IBaseService<Feedback>
    {
<<<<<<< HEAD
        Task CreateFeedbackAsync(CreateFeedbackDto feedbackDto);
=======
        Task<IEnumerable<FeedbackVm>> GetAllFeedbacksAsync();
        Task<FeedbackVm> GetFeedbackByIdAsync(int id);
        Task<bool> AddFeedbackAsync(FeedbackVm feedbackVm);

        Task<bool> UpdateFeedback(int id, UpdateFeedbackVm updateFeedbackVm);
        Task<bool> DeleteFeedbackAsync(int id);
>>>>>>> main
        Task<FeedbackDetailDto> GetFeedbackByAppointmentAsync(int appointmentId);
    }
}