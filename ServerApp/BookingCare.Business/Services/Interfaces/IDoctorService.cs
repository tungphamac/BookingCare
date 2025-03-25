using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IDoctorService : IBaseService<Doctor>
    {
        /// <summary>
        /// Retrieves detailed information of a doctor by ID.
        /// </summary>
        /// <param name="id">The ID of the doctor (UserId).</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the doctor details.</returns>
        Task<DoctorDetailDto?> GetDoctorDetailAsync(int id);
    }
}