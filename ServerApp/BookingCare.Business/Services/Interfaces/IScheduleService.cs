using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IScheduleService : IBaseService<Schedule>
    {
        Task<ScheduleDetailDto?> GetScheduleByIdAsync(int id);
        Task<List<ScheduleDetailDto>> GetAllSchedulesAsync();
        Task<int> CreateScheduleAsync(CreateScheduleDto scheduleDto, int doctorId); // Sửa DTO
        Task<bool> UpdateScheduleAsync(int id, UpdateScheduleDto scheduleDto);
        Task<bool> DeleteScheduleAsync(int id);
        Task<List<ScheduleDetailDto>> GetSchedulesByDoctorIdAsync(int doctorId);

    }
}