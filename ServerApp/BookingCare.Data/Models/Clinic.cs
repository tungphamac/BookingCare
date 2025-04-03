namespace BookingCare.Data.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; } 
        public string Introduction { get; set; }
        public DateTime CreateAt { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();// thêm liên kết 
    }
}