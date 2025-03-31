namespace BookingCare.Business.ViewModels
{
    public class FeedbackVm
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
