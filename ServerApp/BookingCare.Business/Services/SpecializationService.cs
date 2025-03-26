using BookingCare.Data.Models;
using BookingCare.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class SpecializationService
    {
        private readonly SpecializationRepository _repository;

        public SpecializationService(SpecializationRepository repository)
        {
            _repository = repository;
        }

        // Lấy tất cả chuyên khoa
        public async Task<List<Specialization>> GetAllSpecializationsAsync()
        {
            return await _repository.GetAllSpecializationsAsync();
        }

        // Lấy chuyên khoa theo ID
        public async Task<Specialization> GetSpecializationByIdAsync(int id)
        {
            return await _repository.GetSpecializationByIdAsync(id);
        }

        // Thêm chuyên khoa
        public async Task AddSpecializationAsync(Specialization specialization)
        {
            await _repository.AddSpecializationAsync(specialization);
        }

        // Cập nhật chuyên khoa
        public async Task UpdateSpecializationAsync(Specialization specialization)
        {
            await _repository.UpdateSpecializationAsync(specialization);
        }

        // Xóa chuyên khoa
        public async Task DeleteSpecializationAsync(int id)
        {
            await _repository.DeleteSpecializationAsync(id);
        }
    }

}
