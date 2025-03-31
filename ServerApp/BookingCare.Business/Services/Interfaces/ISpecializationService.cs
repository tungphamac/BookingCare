using BookingCare.API.Dtos;
using BookingCare.Business.Services.Base;
using BookingCare.Data.Models;

namespace BookingCare.Business.Services.Interfaces
{
    public interface ISpecializationService : IBaseService<Specialization>
    {
        Task<SpecializationDetailDto?> GetSpecializationByIdAsync(int id);
        Task<List<SpecializationDetailDto>> GetAllSpecializationsAsync();
        Task<int> CreateSpecializationAsync(SpecializationDetailDto specializationDto);
        Task<bool> UpdateSpecializationAsync(int id, SpecializationDetailDto specializationDto);
        Task<bool> DeleteSpecializationAsync(int id);
<<<<<<< HEAD
=======
        Task<ICollection<SpecializationDetailDto>> GetTopSpecializationsAsync(int top);
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
    }
}