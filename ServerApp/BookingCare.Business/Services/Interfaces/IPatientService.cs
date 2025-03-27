using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IPatientService : IBaseService<Patient>
    {
<<<<<<< HEAD
        /// <summary>
        /// Retrieves detailed information of a patient by ID.
        /// </summary>
        /// <param name="id">The ID of the patient (UserId).</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the patient details.</returns>
=======
>>>>>>> main
        Task<PatientDetailDto?> GetPatientDetailAsync(int id);
        Task<IEnumerable<PatientDetailDto>> GetAllAsync();
        Task<int> AddPatientAsync(Patient patient);
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeleteAsync(Patient patient);
<<<<<<< HEAD

=======
        Task<bool> LockUserAccountAsync(int userId, DateTime lockUntil); // Thêm phương thức khóa/mở khóa
>>>>>>> main
    }
}