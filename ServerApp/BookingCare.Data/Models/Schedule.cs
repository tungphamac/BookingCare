namespace BookingCare.Data.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string TimeSlot { get; set; }
        public DateTime WorkDate { get; set; }
        public ScheduleStatus Status { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
    public enum ScheduleStatus
    {
        Available,
        Booked,
        Cancelled
    }
}
