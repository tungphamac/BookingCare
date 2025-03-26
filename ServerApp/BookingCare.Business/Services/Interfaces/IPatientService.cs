using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.ViewModels;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IPatientService : IBaseService<Patient>
    {
        /// <summary>
        /// Retrieves detailed information of a patient by ID.
        /// </summary>
        /// <param name="id">The ID of the patient (UserId).</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the patient details.</returns>
        Task<PatientDetailDto?> GetPatientDetailAsync(int id);
        Task<UpdatePartientVm?> UpdatePatientAsync(int id, UpdatePartientVm patient);
    }
}