using BookingCare.Data.Data;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Data.Repositories
{
    public class SpecializationRepository
    {
        private readonly AppDbContext _context;

        public SpecializationRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả chuyên khoa
        public async Task<List<Specialization>> GetAllSpecializationsAsync()
        {
            return await _context.Specializations.ToListAsync();
        }

        // Lấy chuyên khoa theo ID
        public async Task<Specialization> GetSpecializationByIdAsync(int id)
        {
            return await _context.Specializations.FindAsync(id);
        }

        // Thêm chuyên khoa
        public async Task AddSpecializationAsync(Specialization specialization)
        {
            await _context.Specializations.AddAsync(specialization);
            await _context.SaveChangesAsync();
        }

        // Cập nhật chuyên khoa
        public async Task UpdateSpecializationAsync(Specialization specialization)
        {
            _context.Specializations.Update(specialization);
            await _context.SaveChangesAsync();
        }

        // Xóa chuyên khoa
        public async Task DeleteSpecializationAsync(int id)
        {
            var specialization = await GetSpecializationByIdAsync(id);
            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                await _context.SaveChangesAsync();
            }
        }
    }
}