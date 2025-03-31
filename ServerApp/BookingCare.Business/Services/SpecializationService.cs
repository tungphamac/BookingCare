using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class SpecializationService : BaseService<Specialization>, ISpecializationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SpecializationService> _logger;

        public SpecializationService(ILogger<SpecializationService> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<SpecializationDetailDto?> GetSpecializationByIdAsync(int id)
        {
            try
            {
                var specialization = await _unitOfWork.SpecializationRepository
                    .GetQuery(s => s.Id == id)
                    .Select(s => new SpecializationDetailDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Image = s.Image
                    })
                    .FirstOrDefaultAsync();

                if (specialization == null)
                {
                    _logger.LogWarning($"Specialization with ID {id} not found.");
                    return null;
                }

                return specialization;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving specialization with ID {id}.");
                throw;
            }
        }

        public async Task<List<SpecializationDetailDto>> GetAllSpecializationsAsync()
        {
            try
            {
                var specializations = await _unitOfWork.SpecializationRepository
                    .GetQuery()
                    .Select(s => new SpecializationDetailDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Image = s.Image
                    })
                    .ToListAsync();

                return specializations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all specializations.");
                throw;
            }
        }

        public async Task<int> CreateSpecializationAsync(SpecializationDetailDto specializationDto)
        {
            try
            {
                var specialization = new Specialization
                {
                    Name = specializationDto.Name,
                    Description = specializationDto.Description,
                    Image = specializationDto.Image
                };

                await _unitOfWork.SpecializationRepository.AddAsync(specialization);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Specialization {specialization.Name} created successfully.");
                return specialization.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating specialization {specializationDto.Name}.");
                throw;
            }
        }

        public async Task<bool> UpdateSpecializationAsync(int id, SpecializationDetailDto specializationDto)
        {
            try
            {
                var specialization = await _unitOfWork.SpecializationRepository.GetByIdAsync(id);
                if (specialization == null)
                {
                    _logger.LogWarning($"Specialization with ID {id} not found.");
                    return false;
                }

                specialization.Name = specializationDto.Name;
                specialization.Description = specializationDto.Description;
                specialization.Image = specializationDto.Image;

                _unitOfWork.SpecializationRepository.Update(specialization);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Specialization with ID {id} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating specialization with ID {id}.");
                throw;
            }
        }

        public async Task<bool> DeleteSpecializationAsync(int id)
        {
            try
            {
                var specialization = await _unitOfWork.SpecializationRepository.GetByIdAsync(id);
                if (specialization == null)
                {
                    _logger.LogWarning($"Specialization with ID {id} not found.");
                    return false;
                }

                // Kiểm tra xem specialization có bác sĩ nào đang sử dụng không
                var doctorsInSpecialization = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.SpecializationId == id)
                    .AnyAsync();

                if (doctorsInSpecialization)
                {
                    _logger.LogWarning($"Cannot delete specialization with ID {id} because it has associated doctors.");
                    throw new InvalidOperationException($"Cannot delete specialization with ID {id} because it has associated doctors.");
                }

                _unitOfWork.SpecializationRepository.Delete(specialization);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Specialization with ID {id} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting specialization with ID {id}.");
                throw;
            }
        }
<<<<<<< HEAD
=======

        public async Task<ICollection<SpecializationDetailDto>> GetTopSpecializationsAsync(int top)
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
                        (app, doc) =>new { doc.SpecializationId, app.AppointmentCount})
                        .GroupBy(d => d.SpecializationId)
                        .Select(g => new
                        {
                            SpecializationId = g.Key,
                            DoctorCount = g.Count()
                        })
                        .OrderByDescending(s => s.DoctorCount)
                        .Join(_unitOfWork.Context.Specializations,
                        specInfo => specInfo.SpecializationId,
                        spec => spec.Id,
                        (specInfo, spec) => new SpecializationDetailDto()
                        {
                            Id = spec.Id,
                            Name = spec.Name,
                            Description = spec.Description,
                            Image = spec.Image,
                        })
                        .ToListAsync();

            return result;
        }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
    }
}