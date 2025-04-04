namespace BookingCare.Data.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();// thêm liên kết
    }
}