using BookingCare.API.Dtos;
using BookingCare.Business.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorVm> GetDoctorByIdAsync(int doctorId);
        Task<DoctorDetailDto?> GetDoctorDetailAsync(int id);
        Task<List<DoctorDetailDto>> GetAllDoctorsAsync();
        Task<int> CreateDoctorAsync(CreateDoctorDto createDoctorDto);
        Task<bool> UpdateDoctorAsync(int doctorId, DoctorUpdateDto doctorUpdateDto);
        Task<bool> DeleteDoctorAsync(int doctorId);
        Task<bool> LockUserAccountAsync(int userId, DateTime lockUntil);
        Task<bool> UpdateDoctorProfileAsync(int doctorId, UpdateDoctorVm updateDoctorVm);
        Task<ICollection<FeaturedDoctorVm>> GetFeaturedDoctors(int top);
        Task<ICollection<TopRatingDoctorVm>> GetTopRatingDoctors(int top);
        Task<List<DoctorDetailDto>> GetDoctorsBySpecializationIdAsync(int specializationId);

        // Thêm phương thức mới
        Task<List<DoctorDetailDto>> GetDoctorsBySpecializationAndClinicAsync(int specializationId, int clinicId);
        Task<bool> UploadAvatarAsync(int doctorId, IFormFile avatarFile);
    }
}