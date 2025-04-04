using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IDoctorService : IBaseService<Doctor>
    {
        Task<DoctorDetailDto?> GetDoctorDetailAsync(int id);
        Task<List<DoctorDetailDto>> GetAllDoctorsAsync(); // Thêm phương thức lấy danh sách bác sĩ
        Task<int> CreateDoctorAsync(CreateDoctorDto createDoctorDto);
        Task<bool> UpdateDoctorAsync(int doctorId, DoctorUpdateDto doctorUpdateDto);
        Task<bool> DeleteDoctorAsync(int doctorId);
        Task<bool> LockUserAccountAsync(int userId, DateTime lockUntil); // Đã có sẵn
        Task<ICollection<FeaturedDoctorVm>> GetFeaturedDoctors(int top);// lấy top doctor được đặt lịch nhiều nhất
        Task<ICollection<TopRatingDoctorVm>> GetTopRatingDoctors(int top);// lấy top doctor có rating trung bình cao nhất
        Task<bool> UpdateDoctorProfileAsync(int doctorId, UpdateDoctorVm updateDoctorVm);// update doctor profile, change avatar
        Task<DoctorVm> GetDoctorByIdAsync(int doctorId);
    }
}