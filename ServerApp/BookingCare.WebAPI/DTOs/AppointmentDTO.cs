namespace BookingCare.WebAPI.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int ScheduleId { get; set; }
        public int ClinicId { get; set; }
    }
}