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
    public class DoctorService : BaseService<Doctor>, IDoctorService
    {
        private readonly UserManager<User> _userManager;

        public DoctorService(ILogger<DoctorService> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
     : base(logger, unitOfWork)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
        public async Task<int> CreateDoctorAsync(CreateDoctorDto createDoctorDto)
        {
            try
            {
                // Tạo User cho Doctor
                var user = new User
                {
                    UserName = createDoctorDto.UserName,
                    Email = createDoctorDto.Email,
                    Gender = createDoctorDto.Gender,
                    Address = createDoctorDto.Address,
                    Avatar = createDoctorDto.Avatar
                };

                // Tạo tài khoản với mật khẩu
                var result = await _userManager.CreateAsync(user, createDoctorDto.Password);
                if (!result.Succeeded)
                {
                    // Log lỗi chi tiết khi tạo tài khoản không thành công
                    _logger.LogError("Failed to create user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new InvalidOperationException($"Error creating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                // Tạo Doctor từ User
                var doctor = new Doctor
                {
                    User = user,
                    Achievement = createDoctorDto.Achievement,
                    Description = createDoctorDto.Description,
                    SpecializationId = createDoctorDto.SpecializationId,
                    ClinicId = createDoctorDto.ClinicId
                };

                await _unitOfWork.DoctorRepository.AddAsync(doctor);
                await _unitOfWork.SaveChangesAsync();

                return doctor.UserId; // Trả về UserId của Doctor vừa tạo
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new doctor.");
                throw;
            }
        }
        public async Task<bool> UpdateDoctorAsync(int doctorId, DoctorUpdateDto doctorUpdateDto)
        {
            try
            {
                // Lấy thông tin bác sĩ và người dùng tương ứng
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == doctorId)
                    .Include(d => d.User)  // Bao gồm thông tin người dùng (User)
                    .Include(d => d.Specialization)
                    .Include(d => d.Clinic)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning($"Doctor with UserId {doctorId} not found.");
                    return false;
                }

                // Cập nhật thông tin bác sĩ
                doctor.Achievement = doctorUpdateDto.Achievement;
                doctor.Description = doctorUpdateDto.Description;
                doctor.SpecializationId = doctorUpdateDto.SpecializationId;
                doctor.ClinicId = doctorUpdateDto.ClinicId;

                // Cập nhật thông tin người dùng
                doctor.User.UserName = doctorUpdateDto.UserName;
                doctor.User.Email = doctorUpdateDto.Email;
                doctor.User.Gender = doctorUpdateDto.Gender;
                doctor.User.Address = doctorUpdateDto.Address;
                doctor.User.Avatar = doctorUpdateDto.Avatar;

                // Cập nhật vào cơ sở dữ liệu
                _unitOfWork.DoctorRepository.Update(doctor);
                _unitOfWork.UserRepository.Update(doctor.User);  // Cập nhật thông tin người dùng

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating doctor with UserId {doctorId}.");
                throw;
            }
        }

        public async Task<bool> DeleteDoctorAsync(int doctorId)
        {
            try
            {
                // Lấy thông tin bác sĩ và người dùng tương ứng
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == doctorId)
                    .Include(d => d.User)  // Bao gồm thông tin người dùng (User)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning($"Doctor with UserId {doctorId} not found.");
                    return false;
                }

                // Xóa bác sĩ
                _unitOfWork.DoctorRepository.Delete(doctor);

                // Xóa người dùng (User) liên quan đến bác sĩ
                _unitOfWork.UserRepository.Delete(doctor.User);

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting doctor with UserId {doctorId}.");
                throw;
            }
        }

        public async Task<bool> LockUserAccountAsync(int userId, DateTime lockUntil)
        {
            try
            {
                // Lấy thông tin người dùng từ UserManager
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    _logger.LogWarning($"User with Id {userId} not found.");
                    return false;
                }

                // Thiết lập khóa tài khoản
                user.LockoutEnabled = true; // Bật khóa tài khoản
                user.LockoutEnd = lockUntil; // Đặt thời gian khóa tài khoản

                // Cập nhật người dùng
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
