namespace BookingCare.API.Dtos
{
    public class PatientDetailDto
    {
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public int MedicalRecordId { get; set; }
    }
}