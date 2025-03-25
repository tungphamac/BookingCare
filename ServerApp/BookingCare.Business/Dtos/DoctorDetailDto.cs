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
}