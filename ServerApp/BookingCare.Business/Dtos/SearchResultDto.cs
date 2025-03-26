namespace BookingCare.API.Dtos
{
    public class SearchResultDto
    {
        public string Message { get; set; }
        public List<DoctorSearchDto> Doctors { get; set; } = new List<DoctorSearchDto>();
        public List<ClinicSearchDto> Clinics { get; set; } = new List<ClinicSearchDto>();
        public List<SpecializationSearchDto> Specializations { get; set; } = new List<SpecializationSearchDto>();
    }

    public class DoctorSearchDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SpecializationName { get; set; }
        public string ClinicName { get; set; }
    }

    public class ClinicSearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class SpecializationSearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}