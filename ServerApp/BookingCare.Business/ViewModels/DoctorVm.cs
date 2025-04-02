namespace BookingCare.Business.ViewModels
{
    public class DoctorVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Achievement { get; set; }
        public string Description { get; set; }
        public int SpecializationId { get; set; }
        public int ClinicId { get; set; }
    }
}
