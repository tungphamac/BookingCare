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

        // Phương thức để lấy chuyên khoa theo tên
        public async Task<Specialization?> GetSpecializationByNameAsync(string specializationName)
        {
            return await _context.Specializations.FirstOrDefaultAsync(s => s.Name == specializationName);
        }
    }

}
