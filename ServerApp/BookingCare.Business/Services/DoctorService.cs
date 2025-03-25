using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class DoctorService : BaseService<Doctor>, IDoctorService
    {
        public DoctorService(ILogger<DoctorService> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork)
        {
        }

        public async Task<DoctorDetailDto?> GetDoctorDetailAsync(int id)
        {
            try
            {
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == id)
                    .Include(d => d.User)
                    .Include(d => d.Specialization)
                    .Include(d => d.Clinic)
                    .Select(d => new DoctorDetailDto
                    {
                        UserId = d.UserId,
                        UserName = d.User.UserName,
                        Email = d.User.Email,
                        Gender = d.User.Gender,
                        Address = d.User.Address,
                        Avatar = d.User.Avatar,
                        Achievement = d.Achievement,
                        Description = d.Description,
                        SpecializationName = d.Specialization.Name,
                        ClinicName = d.Clinic.Name
                    })
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning($"Doctor with ID {id} not found.");
                    return null;
                }

                return doctor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving doctor details for ID {id}.");
                throw;
            }
        }
    }
}