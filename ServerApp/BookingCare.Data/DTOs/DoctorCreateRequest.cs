using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Data.DTOs
{
    public class DoctorCreateRequest
    {
        public UserDTO User { get; set; }
        public string Achievement { get; set; }
        public string Description { get; set; }
        public int SpecializationId { get; set; }
        public int ClinicId { get; set; }
    }

    public class UserDTO
    {
         public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
    }
    public class UserUpdateDto
    {
        public int UserId { get; set; } // Thêm UserId để so sánh với id từ URL
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
    }
    public class ClinicDoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public List<string> Doctors { get; set; } // Danh sách tên bác sĩ (Doctor)
    }
    public class ClinicCreateRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Introduction { get; set; }
        public int Phone { get; set; }
        public List<int> DoctorIds { get; set; } // Danh sách Doctor ID liên quan đến phòng khám
    }
    public class ClinicUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public List<int> DoctorIds { get; set; } // Danh sách Doctor ID liên quan đến phòng khám
    }
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }


}
