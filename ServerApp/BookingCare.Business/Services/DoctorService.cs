using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
<<<<<<< HEAD
=======
using BookingCare.Business.ViewModels;
>>>>>>> main
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
<<<<<<< HEAD

        public DoctorService(ILogger<DoctorService> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
     : base(logger, unitOfWork)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }


=======
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(ILogger<DoctorService> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
            : base(logger, unitOfWork)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

>>>>>>> main
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
<<<<<<< HEAD
=======

        public async Task<List<DoctorDetailDto>> GetAllDoctorsAsync()
        {
            try
            {
                var doctors = await _unitOfWork.DoctorRepository
                    .GetQuery()
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
                    .ToListAsync();

                return doctors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all doctors.");
                throw;
            }
        }

>>>>>>> main
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
<<<<<<< HEAD
                    // Log lỗi chi tiết khi tạo tài khoản không thành công
=======
>>>>>>> main
                    _logger.LogError("Failed to create user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    throw new InvalidOperationException($"Error creating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

<<<<<<< HEAD
                // Tạo Doctor từ User
                var doctor = new Doctor
                {
=======
                // Gán vai trò Doctor cho user
                await _userManager.AddToRoleAsync(user, "Doctor");

                // Tạo Doctor từ User
                var doctor = new Doctor
                {
                    UserId = user.Id, // Gán UserId từ user vừa tạo
>>>>>>> main
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
<<<<<<< HEAD
=======

>>>>>>> main
        public async Task<bool> UpdateDoctorAsync(int doctorId, DoctorUpdateDto doctorUpdateDto)
        {
            try
            {
<<<<<<< HEAD
                // Lấy thông tin bác sĩ và người dùng tương ứng
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == doctorId)
                    .Include(d => d.User)  // Bao gồm thông tin người dùng (User)
=======
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == doctorId)
                    .Include(d => d.User)
>>>>>>> main
                    .Include(d => d.Specialization)
                    .Include(d => d.Clinic)
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning($"Doctor with UserId {doctorId} not found.");
                    return false;
                }

<<<<<<< HEAD
                // Cập nhật thông tin bác sĩ
=======
>>>>>>> main
                doctor.Achievement = doctorUpdateDto.Achievement;
                doctor.Description = doctorUpdateDto.Description;
                doctor.SpecializationId = doctorUpdateDto.SpecializationId;
                doctor.ClinicId = doctorUpdateDto.ClinicId;

<<<<<<< HEAD
                // Cập nhật thông tin người dùng
=======
>>>>>>> main
                doctor.User.UserName = doctorUpdateDto.UserName;
                doctor.User.Email = doctorUpdateDto.Email;
                doctor.User.Gender = doctorUpdateDto.Gender;
                doctor.User.Address = doctorUpdateDto.Address;
                doctor.User.Avatar = doctorUpdateDto.Avatar;

<<<<<<< HEAD
                // Cập nhật vào cơ sở dữ liệu
                _unitOfWork.DoctorRepository.Update(doctor);
                _unitOfWork.UserRepository.Update(doctor.User);  // Cập nhật thông tin người dùng
=======
                _unitOfWork.DoctorRepository.Update(doctor);
                _unitOfWork.UserRepository.Update(doctor.User);
>>>>>>> main

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
<<<<<<< HEAD
                // Lấy thông tin bác sĩ và người dùng tương ứng
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == doctorId)
                    .Include(d => d.User)  // Bao gồm thông tin người dùng (User)
=======
                var doctor = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == doctorId)
                    .Include(d => d.User)
>>>>>>> main
                    .FirstOrDefaultAsync();

                if (doctor == null)
                {
                    _logger.LogWarning($"Doctor with UserId {doctorId} not found.");
                    return false;
                }

<<<<<<< HEAD
                // Xóa bác sĩ
                _unitOfWork.DoctorRepository.Delete(doctor);

                // Xóa người dùng (User) liên quan đến bác sĩ
=======
                _unitOfWork.DoctorRepository.Delete(doctor);
>>>>>>> main
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
<<<<<<< HEAD
                // Lấy thông tin người dùng từ UserManager
=======
>>>>>>> main
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    _logger.LogWarning($"User with Id {userId} not found.");
                    return false;
                }

<<<<<<< HEAD
                // Thiết lập khóa tài khoản
                user.LockoutEnabled = true; // Bật khóa tài khoản
                user.LockoutEnd = lockUntil; // Đặt thời gian khóa tài khoản

                // Cập nhật người dùng
=======
                user.LockoutEnabled = true;
                user.LockoutEnd = lockUntil;

>>>>>>> main
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

<<<<<<< HEAD





    }

}
=======
        public async Task<ICollection<FeaturedDoctorVm>> GetFeaturedDoctors(int top)
        {
            var result = await _unitOfWork.Context.Appointments
                        .GroupBy(a => a.DoctorId)
                        .Select(g => new
                        {
                            DoctorId = g.Key,
                            AppointmentCount = g.Count()
                        })
                        .OrderByDescending(g => g.AppointmentCount)
                        .Take(top)
                        .Join(_unitOfWork.Context.Doctors
                        , app => app.DoctorId
                        , doc => doc.UserId
                        , (app, doc) => new { doc, app.AppointmentCount })
                        .Join(_unitOfWork.Context.Users
                        , docInfo => docInfo.doc.UserId
                        , user => user.Id
                        , (docInfo, user) => new FeaturedDoctorVm
                        {
                            DoctorName = user.UserName,
                            Description = docInfo.doc.Description,
                            Achievement = docInfo.doc.Achievement,
                            Address = user.Address,
                            Avatar = user.Avatar
                        })
                        .ToListAsync();

            return result;
        }
    }
}
>>>>>>> main
