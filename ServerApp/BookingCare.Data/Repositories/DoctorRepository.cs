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
    public class DoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all doctors
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                                 .Include(d => d.User)
                                 .Include(d => d.Specialization)
                                 .Include(d => d.Clinic)
                                 .ToListAsync();
        }

        // Get doctor by ID
        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Specialization)
                .Include(d => d.Clinic)
                .FirstOrDefaultAsync(d => d.UserId == id);
        }


        // Add a new doctor
        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        // Update a doctor
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete a doctor
        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await GetDoctorByIdAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
