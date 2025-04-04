namespace BookingCare.API.Dtos
{
    public class UpdateScheduleDto
    {
        public string TimeSlot { get; set; }
        public DateTime WorkDate { get; set; }
        public string Status { get; set; }
    }
}