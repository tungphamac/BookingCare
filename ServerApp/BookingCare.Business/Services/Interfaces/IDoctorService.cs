using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
<<<<<<< HEAD
=======
using BookingCare.Business.ViewModels;
>>>>>>> main
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IDoctorService : IBaseService<Doctor>
    {
<<<<<<< HEAD
        /// <summary>
        /// Retrieves detailed information of a doctor by ID.
        /// </summary>
        /// <param name="id">The ID of the doctor (UserId).</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the doctor details.</returns>
        Task<DoctorDetailDto?> GetDoctorDetailAsync(int id);
        Task<int> CreateDoctorAsync(CreateDoctorDto createDoctorDto);
        Task<bool> UpdateDoctorAsync(int doctorId, DoctorUpdateDto doctorUpdateDto);
        Task<bool> DeleteDoctorAsync(int doctorId);
=======
        Task<DoctorDetailDto?> GetDoctorDetailAsync(int id);
        Task<List<DoctorDetailDto>> GetAllDoctorsAsync(); // Thêm phương thức lấy danh sách bác sĩ
        Task<int> CreateDoctorAsync(CreateDoctorDto createDoctorDto);
        Task<bool> UpdateDoctorAsync(int doctorId, DoctorUpdateDto doctorUpdateDto);
        Task<bool> DeleteDoctorAsync(int doctorId);
        Task<bool> LockUserAccountAsync(int userId, DateTime lockUntil); // Đã có sẵn
        Task<ICollection<FeaturedDoctorVm>> GetFeaturedDoctors(int top);// lấy top doctor được đặt lịch nhiều nhất
>>>>>>> main
    }
}