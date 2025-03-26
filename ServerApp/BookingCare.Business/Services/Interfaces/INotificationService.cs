using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface INotificationService : IBaseService<Notification>
    {
        Task CreateNotificationAsync(int userId, string message, int appointmentId);
        Task<List<NotificationDto>> GetNotificationsAsync(int userId);
        Task<AppointmentDetailDto> GetAppointmentDetailAsync(int appointmentId);
        Task RespondToAppointmentAsync(int appointmentId, bool accept);
    }
}