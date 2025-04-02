using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IPatientService : IBaseService<Patient>
    {
        Task<PatientDetailDto?> GetPatientDetailAsync(int id);
        Task<PatientDetailDto?> GetPatientByGmailAsync(string email);

        Task<IEnumerable<PatientDetailDto>> GetAllAsync();
        Task<int> AddPatientAsync(Patient patient);
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeleteAsync(Patient patient);
        Task<bool> LockUserAccountAsync(int userId, DateTime lockUntil); // Thêm phương thức khóa/mở khóa
    }
}