using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
<<<<<<< HEAD
=======
using BookingCare.Business.ViewModels;
>>>>>>> main
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IClinicService : IBaseService<Clinic>
    {
<<<<<<< HEAD
        /// <summary>
        /// Retrieves detailed information of a clinic by ID.
        /// </summary>
        /// <param name="id">The ID of the clinic.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the clinic details.</returns>
        Task<ClinicDetailDto?> GetClinicDetailAsync(int id);
=======
        Task<ClinicDetailDto> GetClinicByIdAsync(int id);
        Task<List<ClinicDetailDto>> GetAllClinicsAsync();
        Task CreateClinicAsync(ClinicDetailDto clinicDto);
        Task UpdateClinicAsync(int id, ClinicDetailDto clinicDto);
        Task DeleteClinicAsync(int id);
        Task<ICollection<ClinicVm>> GetTopClinics(int top);
>>>>>>> main
    }
}