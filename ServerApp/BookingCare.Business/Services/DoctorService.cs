using BookingCare.Business.DTOs;
using BookingCare.Data.DTOs;
using BookingCare.Data.Models;
using BookingCare.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
   public class DoctorService
{
    private readonly DoctorRepository _doctorRepository;
        private readonly UserManager<User> _userManager; // Khai báo UserManager

        public DoctorService(DoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
      
        }

        // Phương thức để lấy tất cả bác sĩ và chuyển thành DTO
        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();

            return doctors.Select(d => new DoctorDto
            {
                UserId = d.UserId,
                Achievement = d.Achievement,
                Description = d.Description,
                SpecializationId = d.SpecializationId,
                ClinicId = (int)d.ClinicId,
                // Lấy thông tin từ bảng User
                UserName = d.User.UserName,
                
                Email = d.User.Email // Thêm các thông tin cần thiết từ bảng User
            });
        }

        // Phương thức để lấy bác sĩ theo ID
        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.GetDoctorByIdAsync(id);
        }


        // Phương thức để thêm bác sĩ
        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _doctorRepository.AddDoctorAsync(doctor);
        }


        // Phương thức để cập nhật bác sĩ
        //public async Task UpdateDoctorAsync(int id, Doctor doctor)
        //{
        //    var existingDoctor = await _doctorRepository.GetDoctorByIdAsync(id);
        //    if (existingDoctor == null)
        //    {
        //        throw new Exception("Doctor not found.");
        //    }

        //    // Cập nhật các thuộc tính của Doctor
        //    existingDoctor.Achievement = doctor.Achievement;
        //    existingDoctor.Description = doctor.Description;
        //    existingDoctor.SpecializationId = doctor.SpecializationId;
        //    existingDoctor.ClinicId = doctor.ClinicId;

        //    // Cập nhật thông tin người dùng (User)
        //    var user = existingDoctor.User;
        //    user.Email = doctor.User.Email ?? user.Email;
        //    user.PhoneNumber = doctor.User.PhoneNumber ?? user.PhoneNumber;
        //    user.Gender = doctor.User.Gender ?? user.Gender;
        //    user.Address = doctor.User.Address ?? user.Address;
        //    user.Avatar = doctor.User.Avatar ?? user.Avatar;

        //    await _doctorRepository.UpdateDoctorAsync(existingDoctor);
        //}

        // Phương thức để xóa bác sĩ
        public async Task DeleteDoctorAsync(int id)
        {
            await _doctorRepository.DeleteDoctorAsync(id);
        }
        public async Task LockDoctorAsync(int doctorId)
        {
            await _doctorRepository.LockDoctorAsync(doctorId);
        }

        // Phương thức mở khóa tài khoản
        public async Task UnlockDoctorAsync(int doctorId)
        {
            await _doctorRepository.UnlockDoctorAsync(doctorId);
        }

    }


}
