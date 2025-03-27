using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IScheduleService : IBaseService<Schedule>
    {
        Task<ScheduleDetailDto?> GetScheduleByIdAsync(int id);
        Task<List<ScheduleDetailDto>> GetAllSchedulesAsync();
        Task<int> CreateScheduleAsync(ScheduleDetailDto scheduleDto, int doctorId);
        Task<bool> UpdateScheduleAsync(int id, ScheduleDetailDto scheduleDto, int doctorId);
        Task<bool> DeleteScheduleAsync(int id, int doctorId);
    }
}