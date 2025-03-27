namespace BookingCare.API.Dtos
{
    public class CreateFeedbackDto
    {
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

    public class FeedbackDetailDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}