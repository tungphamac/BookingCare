namespace BookingCare.API.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int AppointmentId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AppointmentDetailDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime ScheduleTime { get; set; }
<<<<<<< HEAD
        public string Reason { get; set; }
        public int ClinicId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        
=======
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
    }
}