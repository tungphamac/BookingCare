using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class PatientService : BaseService<Patient>, IPatientService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PatientService> _logger;

        public PatientService(ILogger<PatientService> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
            : base(logger, unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientDetailDto>> GetAllAsync()
        {
            var patients = await _unitOfWork.PatientRepository
                .GetQuery()
                .Include(p => p.User)
                .ToListAsync();

            if (patients == null || !patients.Any())
            {
                return new List<PatientDetailDto>();
            }

            return patients.Select(p => new PatientDetailDto
            {
               
                UserName = p.User?.UserName ?? "No name",
                Email = p.User?.Email ?? "No email",
                Gender = p.User?.Gender ?? false,
                Address = p.User?.Address ?? "No address",
                Avatar = p.User?.Avatar ?? "default.jpg",
                MedicalRecordId = p.MedicalRecordId
            }).ToList();
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

        public async Task<int> AddPatientAsync(Patient patient)
        {
            if (patient != null)
            {
                await _unitOfWork.PatientRepository.AddAsync(patient);
                return await _unitOfWork.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            if (patient != null)
            {
                _unitOfWork.PatientRepository.Update(patient);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Patient patient)
        {
            if (patient != null)
            {
                _unitOfWork.PatientRepository.Delete(patient);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> LockUserAccountAsync(int userId, DateTime lockUntil)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    _logger.LogWarning($"User with Id {userId} not found.");
                    return false;
                }

                user.LockoutEnabled = true;
                user.LockoutEnd = lockUntil;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User with Id {userId} has been locked until {lockUntil}.");
                    return true;
                }

                _logger.LogError($"Failed to lock account for user {userId}.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error locking account for user {userId}.");
                throw;
            }
        }
    }
}