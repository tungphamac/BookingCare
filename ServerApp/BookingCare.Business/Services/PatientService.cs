using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Business.ViewModels;
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

        public async Task<UpdatePartientVm?> UpdatePatientAsync(int id, UpdatePartientVm patient)
        {
            try
            {
                // Tìm Patient theo UserId
                var existingPatient = await _unitOfWork.PatientRepository
                    .GetQuery(p => p.UserId == id)
                    .Include(p => p.User)
                    .FirstOrDefaultAsync();

                if (existingPatient == null)
                {
                    _logger.LogWarning($"Patient with ID {id} not found.");
                    return null;
                }

                // Cập nhật thông tin User
                var user = existingPatient.User;
                user.UserName = patient.UserName ?? user.UserName;
                user.Email = patient.Email ?? user.Email;
                user.Gender = patient.Gender; // Giả sử Gender là thuộc tính của User
                user.Address = patient.Address ?? user.Address;
                user.Avatar = patient.Avatar ?? user.Avatar;

                // Nếu Phone là thuộc tính của User hoặc Patient
                // Giả sử Phone thuộc User, nếu thuộc Patient thì thêm vào entity Patient
                if (!string.IsNullOrEmpty(patient.Phone))
                {
                    // Cập nhật Phone vào User (nếu User có thuộc tính PhoneNumber)
                    // Nếu không, cần thêm Phone vào Patient entity
                    user.PhoneNumber = patient.Phone;
                }

                // Cập nhật User qua UnitOfWork
                _unitOfWork.UserRepository.Update(user);

                // Lưu thay đổi vào database
                await _unitOfWork.SaveChangesAsync();

                // Trả về thông tin vừa cập nhật
                return new UpdatePartientVm
                {
                    UserName = user.UserName,
                    Gender = user.Gender,
                    Address = user.Address,
                    Phone = user.PhoneNumber, // Nếu không có PhoneNumber, bỏ dòng này
                    Email = user.Email,
                    Avatar = user.Avatar
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating patient with ID {id}.");
                throw;
            }
        }
    }
}