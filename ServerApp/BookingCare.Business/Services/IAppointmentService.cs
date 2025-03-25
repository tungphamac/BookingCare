using BookingCare.Data.Models;

namespace BookingCare.Business.Services
{
    public interface IAppointmentService
    {
        Task<int> CreateAppointmentAsync(Appointment appointment);
        Task<bool> ManageAppointmentAsync(int appointmentId, string status, int userId);
        Task<bool> UpdateAppointmentAsync(Appointment appointment, int patientId);
        Task<Appointment?> GetAppointmentDetailAsync(int appointmentId, int userId);
    }
}