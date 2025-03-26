using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IClinicService : IBaseService<Clinic>
    {
        /// <summary>
        /// Retrieves detailed information of a clinic by ID.
        /// </summary>
        /// <param name="id">The ID of the clinic.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the clinic details.</returns>
        Task<ClinicDetailDto?> GetClinicDetailAsync(int id);
    }
}