using BookingCare.Data.Data;
using BookingCare.Data.DTOs;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Data.Repositories
{
    public class ClinicRepository
    {
        private readonly AppDbContext _context;

        public ClinicRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách phòng khám với tên bác sĩ (Doctor)
        public async Task<IEnumerable<ClinicDoctorDto>> GetClinicsWithDoctorsAsync()
        {
            var clinicsWithDoctors = await _context.Clinics
                .Include(c => c.Doctors) // Lấy các bác sĩ liên quan đến phòng khám
                .Select(c => new ClinicDoctorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Phone = c.Phone,
                    Doctors = c.Doctors.Select(d => d.User.UserName).ToList() // Lấy tên bác sĩ (Doctor)
                })
                .ToListAsync();

            return clinicsWithDoctors;
        }

        // Lấy phòng khám theo ID (có thể không cần join Doctor nếu không cần)
        public async Task<Clinic?> GetClinicByIdAsync(int id)
        {
            return await _context.Clinics
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddClinicAsync(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();
        }
        // Thêm phòng khám mới
        public async Task AddClinicAsync(Clinic clinic, List<int> doctorIds)
        {
            _context.Clinics.Add(clinic);

            // Lấy danh sách bác sĩ từ DoctorIds và gán vào phòng khám
            var doctors = await _context.Doctors
                                         .Where(d => doctorIds.Contains(d.UserId))
                                         .ToListAsync();

            clinic.Doctors = doctors;

            await _context.SaveChangesAsync();
        }

        // Cập nhật phòng khám
        public async Task UpdateClinicAsync(Clinic clinic, List<int> doctorIds)
        {
            _context.Clinics.Update(clinic);

            // Lấy danh sách bác sĩ từ DoctorIds và gán vào phòng khám
            var doctors = await _context.Doctors
                                         .Where(d => doctorIds.Contains(d.UserId))
                                         .ToListAsync();

            clinic.Doctors = doctors;

            await _context.SaveChangesAsync();
        }
    
    public async Task DeleteClinicAsync(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic != null)
            {
                // Xóa các bác sĩ liên kết với phòng khám (nếu cần)
                var doctors = await _context.Doctors
                                             .Where(d => d.ClinicId == id)
                                             .ToListAsync();
                foreach (var doctor in doctors)
                {
                    doctor.ClinicId = null; // Xóa liên kết với phòng khám (nếu cần)
                }

                _context.Clinics.Remove(clinic);
                await _context.SaveChangesAsync();
            }
        }

    }
}
