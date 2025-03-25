using BookingCare.Business.DTOs;
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

        // Phương thức để lấy tất cả bác sĩ
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Specialization)
                .Include(d => d.Clinic)
                .ToListAsync();
        }

        // Phương thức để lấy bác sĩ theo ID
        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.User) // Join với bảng User
                .Include(d => d.Specialization)
                .Include(d => d.Clinic)
                .FirstOrDefaultAsync(d => d.UserId == id);
        }

        // Phương thức để thêm bác sĩ
        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }


        // Phương thức để cập nhật bác sĩ
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor); // Cập nhật đối tượng Doctor trong cơ sở dữ liệu
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Phương thức để xóa bác sĩ
        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task LockDoctorAsync(int doctorId)
        {
            var doctor = await _context.Doctors.Include(d => d.User)
                                               .FirstOrDefaultAsync(d => d.UserId == doctorId);

            if (doctor != null)
            {
                if (doctor.User.LockoutEnabled && doctor.User.LockoutEnd > DateTime.UtcNow)
                {
                    // Tài khoản đã bị khóa, không cần khóa lại
                    throw new InvalidOperationException("Doctor account is already locked.");
                }

                // Khóa tài khoản
                doctor.User.LockoutEnabled = true;
                doctor.User.LockoutEnd = DateTime.UtcNow.AddMinutes(30); // Khóa tài khoản trong 30 phút
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Doctor not found.");
            }
        }

        // Phương thức mở khóa tài khoản
        public async Task UnlockDoctorAsync(int doctorId)
        {
            var doctor = await _context.Doctors.Include(d => d.User)
                                               .FirstOrDefaultAsync(d => d.UserId == doctorId);

            if (doctor != null)
            {
                // Mở khóa tài khoản
                doctor.User.LockoutEnd = null;
                doctor.User.LockoutEnabled = false; // Tắt tính năng khóa tài khoản
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Doctor not found.");
            }
        }
    }

}
