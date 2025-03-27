using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingCare.Business.Services
{
    public class ScheduleService : BaseService<Schedule>, IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(ILogger<ScheduleService> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ScheduleDetailDto?> GetScheduleByIdAsync(int id)
        {
            try
            {
                var schedule = await _unitOfWork.ScheduleRepository
                    .GetQuery(s => s.Id == id)
                    .Select(s => new ScheduleDetailDto
                    {
                        Id = s.Id,
                        DoctorId = s.DoctorId,
                        TimeSlot = s.TimeSlot,
                        WorkDate = s.WorkDate,
                        Status = s.Status.ToString()
                    })
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    _logger.LogWarning($"Schedule with ID {id} not found.");
                    return null;
                }

                return schedule;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving schedule with ID {id}.");
                throw;
            }
        }

        public async Task<List<ScheduleDetailDto>> GetAllSchedulesAsync()
        {
            try
            {
                var schedules = await _unitOfWork.ScheduleRepository
                    .GetQuery()
                    .Select(s => new ScheduleDetailDto
                    {
                        Id = s.Id,
                        DoctorId = s.DoctorId,
                        TimeSlot = s.TimeSlot,
                        WorkDate = s.WorkDate,
                        Status = s.Status.ToString()
                    })
                    .ToListAsync();

                return schedules;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all schedules.");
                throw;
            }
        }

        public async Task<int> CreateScheduleAsync(ScheduleDetailDto scheduleDto, int doctorId)
        {
            try
            {
                // Kiểm tra xem DoctorId có tồn tại không
                var doctorExists = await _unitOfWork.DoctorRepository
                    .GetQuery(d => d.UserId == doctorId)
                    .AnyAsync();

                if (!doctorExists)
                {
                    _logger.LogWarning($"Doctor with ID {doctorId} not found.");
                    throw new InvalidOperationException($"Doctor with ID {doctorId} not found.");
                }

                var schedule = new Schedule
                {
                    DoctorId = doctorId,
                    TimeSlot = scheduleDto.TimeSlot,
                    WorkDate = scheduleDto.WorkDate,
                    Status = Enum.Parse<ScheduleStatus>(scheduleDto.Status)
                };

                await _unitOfWork.ScheduleRepository.AddAsync(schedule);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Schedule created successfully for Doctor ID {doctorId}.");
                return schedule.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating schedule for Doctor ID {doctorId}.");
                throw;
            }
        }

        public async Task<bool> UpdateScheduleAsync(int id, ScheduleDetailDto scheduleDto, int doctorId)
        {
            try
            {
                var schedule = await _unitOfWork.ScheduleRepository
                    .GetQuery(s => s.Id == id)
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    _logger.LogWarning($"Schedule with ID {id} not found.");
                    return false;
                }

                // Kiểm tra xem lịch có thuộc về bác sĩ này không
                if (schedule.DoctorId != doctorId)
                {
                    _logger.LogWarning($"Doctor with ID {doctorId} is not authorized to update Schedule ID {id}.");
                    throw new UnauthorizedAccessException($"Doctor with ID {doctorId} is not authorized to update this schedule.");
                }

                schedule.TimeSlot = scheduleDto.TimeSlot;
                schedule.WorkDate = scheduleDto.WorkDate;
                schedule.Status = Enum.Parse<ScheduleStatus>(scheduleDto.Status);

                _unitOfWork.ScheduleRepository.Update(schedule);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Schedule ID {id} updated successfully by Doctor ID {doctorId}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating schedule ID {id} by Doctor ID {doctorId}.");
                throw;
            }
        }

        public async Task<bool> DeleteScheduleAsync(int id, int doctorId)
        {
            try
            {
                var schedule = await _unitOfWork.ScheduleRepository
                    .GetQuery(s => s.Id == id)
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    _logger.LogWarning($"Schedule with ID {id} not found.");
                    return false;
                }

                // Kiểm tra xem lịch có thuộc về bác sĩ này không
                if (schedule.DoctorId != doctorId)
                {
                    _logger.LogWarning($"Doctor with ID {doctorId} is not authorized to delete Schedule ID {id}.");
                    throw new UnauthorizedAccessException($"Doctor with ID {doctorId} is not authorized to delete this schedule.");
                }

                // Kiểm tra xem lịch có lịch hẹn nào không
                var hasAppointments = await _unitOfWork.AppointmentRepository
                    .GetQuery(a => a.ScheduleId == id)
                    .AnyAsync();

                if (hasAppointments)
                {
                    _logger.LogWarning($"Cannot delete Schedule ID {id} because it has associated appointments.");
                    throw new InvalidOperationException($"Cannot delete schedule because it has associated appointments.");
                }

                _unitOfWork.ScheduleRepository.Delete(schedule);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Schedule ID {id} deleted successfully by Doctor ID {doctorId}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting schedule ID {id} by Doctor ID {doctorId}.");
                throw;
            }
        }
    }
}