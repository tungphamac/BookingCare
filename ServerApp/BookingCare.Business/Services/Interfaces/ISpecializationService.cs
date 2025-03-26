using BookingCare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services.Interfaces
{
    public interface ISpecializationService
    {
        Task<int> AddAsync(Specialization specialization);
        Task<bool> UpdateAsync(Specialization specialization);
        Task<bool> DeleteAsync(Specialization specialization);
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task<Specialization> GetByIdAsync(int id);
    }

}
