<<<<<<< HEAD
﻿using BookingCare.Data.Models;
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
=======
﻿using BookingCare.API.Dtos;
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
    }
}
>>>>>>> main
