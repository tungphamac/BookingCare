namespace BookingCare.API.Dtos
{
    public class ScheduleDetailDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string TimeSlot { get; set; }
        public DateTime WorkDate { get; set; }
        public string Status { get; set; }
    }
}