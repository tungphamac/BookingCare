namespace BookingCare.Data.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string TimeSlot { get; set; }
        public DateTime WorkDate { get; set; }
        public ScheduleStatus Status { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public DateTime Time { get; set; } //thêm datetime
    }
    public enum ScheduleStatus
    {
        Available,
        Booked,
        Cancelled
    }
}
