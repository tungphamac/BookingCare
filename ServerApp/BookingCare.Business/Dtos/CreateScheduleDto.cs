namespace BookingCare.API.Dtos
{
    public class CreateScheduleDto
    {
        public string TimeSlot { get; set; }
        public DateTime WorkDate { get; set; }
        public string Status { get; set; }
    }
}