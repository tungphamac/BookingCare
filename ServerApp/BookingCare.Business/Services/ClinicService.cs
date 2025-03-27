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
    public class ClinicService : BaseService<Clinic>, IClinicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClinicService> _logger;

        public ClinicService(ILogger<ClinicService> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ClinicDetailDto> GetClinicByIdAsync(int id)
        {
            try
            {
                var clinic = await _unitOfWork.ClinicRepository
                    .GetQuery(c => c.Id == id)
                    .Select(c => new ClinicDetailDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Address = c.Address,
                        Phone = c.Phone,
                        Introduction = c.Introduction, // Sửa từ Description thành Introduction
                        CreateAt = c.CreateAt
                    })
                    .FirstOrDefaultAsync();

                if (clinic == null)
                {
                    _logger.LogWarning($"Clinic with ID {id} not found.");
                    throw new ArgumentException($"Clinic with ID {id} not found.");
                }

                return clinic;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving clinic with ID {id}.");
                throw;
            }
        }

        public async Task<List<ClinicDetailDto>> GetAllClinicsAsync()
        {
            try
            {
                var clinics = await _unitOfWork.ClinicRepository
                    .GetQuery()
                    .Select(c => new ClinicDetailDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Address = c.Address,
                        Phone = c.Phone,
                        Introduction = c.Introduction, // Sửa từ Description thành Introduction
                        CreateAt = c.CreateAt
                    })
                    .ToListAsync();

                return clinics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all clinics.");
                throw;
            }
        }

        public async Task CreateClinicAsync(ClinicDetailDto clinicDto)
        {
            try
            {
                var clinic = new Clinic
                {
                    Name = clinicDto.Name,
                    Address = clinicDto.Address,
                    Phone = clinicDto.Phone,
                    Introduction = clinicDto.Introduction, // Sửa từ Description thành Introduction
                    CreateAt = DateTime.UtcNow // Gán giá trị mặc định cho CreateAt
                };

                await _unitOfWork.ClinicRepository.AddAsync(clinic);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Clinic {clinic.Name} created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating clinic {clinicDto.Name}.");
                throw;
            }
        }

        public async Task UpdateClinicAsync(int id, ClinicDetailDto clinicDto)
        {
            try
            {
                var clinic = await _unitOfWork.ClinicRepository.GetByIdAsync(id);
                if (clinic == null)
                {
                    throw new ArgumentException($"Clinic with ID {id} not found.");
                }

                clinic.Name = clinicDto.Name;
                clinic.Address = clinicDto.Address;
                clinic.Phone = clinicDto.Phone;
                clinic.Introduction = clinicDto.Introduction; // Sửa từ Description thành Introduction
                // Không cập nhật CreateAt vì đây là thời gian tạo, không nên thay đổi

                _unitOfWork.ClinicRepository.Update(clinic);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Clinic with ID {id} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating clinic with ID {id}.");
                throw;
            }
        }

        public async Task DeleteClinicAsync(int id)
        {
            try
            {
                var clinic = await _unitOfWork.ClinicRepository.GetByIdAsync(id);
                if (clinic == null)
                {
                    throw new ArgumentException($"Clinic with ID {id} not found.");
                }

                // Kiểm tra xem clinic có bác sĩ nào đang làm việc không
                var doctorsInClinic = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.ClinicId == id)
                    .AnyAsync();

                if (doctorsInClinic)
                {
                    throw new InvalidOperationException($"Cannot delete clinic with ID {id} because it has associated doctors.");
                }

                _unitOfWork.ClinicRepository.Delete(clinic);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Clinic with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting clinic with ID {id}.");
                throw;
            }
        }

        public async Task<ICollection<ClinicVm>> GetTopClinics(int top)
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
         .Join(_unitOfWork.Context.Doctors,
             app => app.DoctorId,
             doc => doc.UserId,
             (app, doc) => new { doc.ClinicId, app.AppointmentCount })
         .GroupBy(d => d.ClinicId)
         .Select(g => new
         {
             ClinicId = g.Key,
             DoctorCount = g.Count()
         })
         .OrderByDescending(c => c.DoctorCount)
         .Join(_unitOfWork.Context.Clinics,
             clinicInfo => clinicInfo.ClinicId,
             clinic => clinic.Id,
             (clinicInfo, clinic) => new ClinicVm()
             {
                 Name = clinic.Name,
                 Address = clinic.Address,
                 Phone = clinic.Phone,
                 Introduction = clinic.Introduction,
                 CreateAt = clinic.CreateAt,
             })
         .ToListAsync();

            return result;
        }
    }
}