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
    public class PatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách bệnh nhân kèm thông tin User
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                                 .Include(p => p.User)  // Join bảng User
                                 .Select(p => new Patient
                                 {
                                     UserId = p.UserId,
                                     User = new User
                                     {
                                         Id = p.User.Id,
                                         UserName = p.User.UserName,
                                         Email = p.User.Email,
                                         Gender = p.User.Gender,
                                         Address = p.User.Address,
                                         Avatar = p.User.Avatar
                                     },
                                     MedicalRecordId = p.MedicalRecordId,
                                     Appointments = p.Appointments
                                 })
                                 .ToListAsync();
        }

        // Lấy thông tin bệnh nhân theo UserId với thông tin user rút gọn
        public async Task<Patient?> GetPatientByIdAsync(int userId)
        {
            return await _context.Patients
                                 .Include(p => p.User)
                                 .Where(p => p.UserId == userId)
                                 .Select(p => new Patient
                                 {
                                     UserId = p.UserId,
                                     User = new User
                                     {
                                         Id = p.User.Id,
                                         UserName = p.User.UserName,
                                         Email = p.User.Email,
                                         Gender = p.User.Gender,
                                         Address = p.User.Address,
                                         Avatar = p.User.Avatar
                                     },
                                     MedicalRecordId = p.MedicalRecordId,
                                     Appointments = p.Appointments
                                 })
                                 .FirstOrDefaultAsync();
        }

        // Thêm mới bệnh nhân
        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin bệnh nhân
        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        // Xóa bệnh nhân
        public async Task DeletePatientAsync(int userId)
        {
            var patient = await _context.Patients.FindAsync(userId);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
