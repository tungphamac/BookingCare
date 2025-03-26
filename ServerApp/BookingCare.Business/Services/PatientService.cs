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
        private readonly UserManager<User> _userManager; // Khai báo _userManager

        public PatientService(ILogger<PatientService> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
            : base(logger, unitOfWork)
        {
            _userManager = userManager;  // Inject UserManager vào constructor
        }

       

       

        public async Task<IEnumerable<PatientDetailDto>> GetAllAsync()
        {
            // Đảm bảo truy vấn nạp đúng mối quan hệ User
            var patients = await _unitOfWork.PatientRepository
                .GetQuery()  // Lấy tất cả bệnh nhân
                .Include(p => p.User)  // Nạp thông tin người dùng
                .ToListAsync();

            // Nếu không có bệnh nhân nào, trả về thông báo phù hợp
            if (patients == null || !patients.Any())
            {
                return new List<PatientDetailDto>();
            }

            // Nếu có bệnh nhân, chuyển đổi sang PatientDetailDto
            return patients.Select(p => new PatientDetailDto
            {
                UserId = p.UserId,
                UserName = p.User?.UserName ?? "No name",  // Kiểm tra nếu User là null
                Email = p.User?.Email ?? "No email",      // Kiểm tra nếu User là null
                Gender = p.User?.Gender ?? false,          // Kiểm tra nếu User là null
                Address = p.User?.Address ?? "No address", // Kiểm tra nếu User là null
                Avatar = p.User?.Avatar ?? "default.jpg",  // Kiểm tra nếu User là null
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

        public Task<bool> UpdatePatientAsync(int id, Patient updatedPatient)
        {
            throw new NotImplementedException();
        }
        public async Task<int> AddPatientAsync(Patient patient)
        {
            if (patient != null)
            {
                await _unitOfWork.PatientRepository.AddAsync(patient);
                return await _unitOfWork.SaveChangesAsync();  // Lưu vào cơ sở dữ liệu
            }
            return 0;  // Nếu patient null thì trả về 0
        }
        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            if (patient != null)
            {
                _unitOfWork.PatientRepository.Update(patient);  // Cập nhật thông tin bệnh nhân
                return await _unitOfWork.SaveChangesAsync() > 0;  // Lưu thay đổi vào cơ sở dữ liệu
            }
            return false;
        }
        public async Task<bool> DeleteAsync(Patient patient)
        {
            if (patient != null)
            {
                _unitOfWork.PatientRepository.Delete(patient);
                return await _unitOfWork.SaveChangesAsync() > 0;  // Lưu thay đổi vào cơ sở dữ liệu
            }
            return false;
        }

    }
}