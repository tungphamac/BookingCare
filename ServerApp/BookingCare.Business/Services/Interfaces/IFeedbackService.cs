using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IFeedbackService : IBaseService<Feedback>
    {
        Task CreateFeedbackAsync(CreateFeedbackDto feedbackDto);
        Task<FeedbackDetailDto> GetFeedbackByAppointmentAsync(int appointmentId);
    }
}