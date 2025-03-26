using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecializationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Thêm chuyên khoa mới
        public async Task<int> AddAsync(Specialization specialization)
        {
            if (specialization != null)
            {
                await _unitOfWork.SpecializationRepository.AddAsync(specialization);
                return await _unitOfWork.SaveChangesAsync();
            }
            return 0;
        }

        // Cập nhật chuyên khoa
        public async Task<bool> UpdateAsync(Specialization specialization)
        {
            if (specialization != null)
            {
                _unitOfWork.SpecializationRepository.Update(specialization);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            return false;
        }

        // Xóa chuyên khoa
        public async Task<bool> DeleteAsync(Specialization specialization)
        {
            if (specialization != null)
            {
                _unitOfWork.SpecializationRepository.Delete(specialization);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            return false;
        }

        // Lấy tất cả chuyên khoa
        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _unitOfWork.SpecializationRepository.GetAllAsync();
        }

        // Lấy chuyên khoa theo ID
        public async Task<Specialization> GetByIdAsync(int id)
        {
            return await _unitOfWork.SpecializationRepository.GetByIdAsync(id);
        }
    }

}
