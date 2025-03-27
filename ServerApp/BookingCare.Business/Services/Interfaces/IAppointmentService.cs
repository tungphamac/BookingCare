using BookingCare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<int> CreateAppointmentAsync(Appointment appointment);
        Task<bool> ManageAppointmentAsync(int appointmentId, AppointmentStatus status, int userId);
        Task<bool> UpdateAppointmentAsync(Appointment appointment, int patientId);
        Task<Appointment?> GetAppointmentDetailAsync(int appointmentId, int userId);
    }
}
