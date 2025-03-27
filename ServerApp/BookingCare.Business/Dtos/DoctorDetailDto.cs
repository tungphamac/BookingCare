namespace BookingCare.API.Dtos
{
    public class DoctorDetailDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Achievement { get; set; }
        public string Description { get; set; }
        public string SpecializationName { get; set; }
        public string ClinicName { get; set; }
    }
    public class CreateDoctorDto
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Thêm mật khẩu
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Achievement { get; set; }
        public string Description { get; set; }
        public int SpecializationId { get; set; }
        public int ClinicId { get; set; }
    }
    public class DoctorUpdateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Achievement { get; set; }
        public string Description { get; set; }
        public int SpecializationId { get; set; }
        public int ClinicId { get; set; }
    }

}