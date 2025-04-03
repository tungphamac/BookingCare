using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IDoctorService : IBaseService<Doctor>
    {
        Task<DoctorDetailDto?> GetDoctorDetailAsync(int id);
        Task<List<DoctorDetailDto>> GetAllDoctorsAsync();
        Task<int> CreateDoctorAsync(CreateDoctorDto createDoctorDto);
        Task<bool> UpdateDoctorAsync(int doctorId, DoctorUpdateDto doctorUpdateDto);
        Task<bool> DeleteDoctorAsync(int doctorId);
        Task<bool> LockUserAccountAsync(int userId, DateTime lockUntil);
        Task<ICollection<FeaturedDoctorVm>> GetFeaturedDoctors(int top);
        Task<ICollection<TopRatingDoctorVm>> GetTopRatingDoctors(int top);
        Task<List<DoctorDetailDto>> GetDoctorsBySpecializationIdAsync(int specializationId); // Thêm phương thức mới
    }
}