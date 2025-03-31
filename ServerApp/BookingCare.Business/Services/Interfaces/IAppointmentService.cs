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
<<<<<<< HEAD
        Task<List<Appointment>> GetAppointmentsAsync(int userId, string role, int pageNumber = 1, int pageSize = 10);
=======
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
    }
}
