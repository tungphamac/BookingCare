using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IClinicService : IBaseService<Clinic>
    {
        Task<ClinicDetailDto> GetClinicByIdAsync(int id);
        Task<List<ClinicDetailDto>> GetAllClinicsAsync();
        Task CreateClinicAsync(ClinicDetailDto clinicDto);
        Task UpdateClinicAsync(int id, ClinicDetailDto clinicDto);
        Task DeleteClinicAsync(int id);
        Task<ICollection<ClinicVm>> GetTopClinics(int top);
        Task<List<DoctorDetailDto>> GetDoctorsByClinicIdAsync(int clinicId);
        Task<List<ClinicDetailDto>> GetClinicsBySpecializationIdAsync(int specializationId); // Thêm phương thức mới
    }
}