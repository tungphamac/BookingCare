using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class PatientService : BaseService<Patient>, IPatientService
    {
        public PatientService(ILogger<PatientService> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork)
        {
        }

        public async Task<PatientDetailDto?> GetPatientDetailAsync(int id)
        {
            try
            {
                var patient = await _unitOfWork.PatientRepository
                    .GetQuery(p => p.UserId == id)
                    .Include(p => p.User)
                    .Select(p => new PatientDetailDto
                    {
                        UserId = p.UserId,
                        UserName = p.User.UserName,
                        Email = p.User.Email,
                        Gender = p.User.Gender,
                        Address = p.User.Address,
                        Avatar = p.User.Avatar,
                        MedicalRecordId = p.MedicalRecordId
                    })
                    .FirstOrDefaultAsync();

                if (patient == null)
                {
                    _logger.LogWarning($"Patient with ID {id} not found.");
                    return null;
                }

                return patient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving patient details for ID {id}.");
                throw;
            }
        }
    }
}