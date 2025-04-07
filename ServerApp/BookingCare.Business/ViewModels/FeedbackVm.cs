namespace BookingCare.Business.ViewModels
{
    public class FeedbackVm
    {

        public int Id { get; set; }
        public string PatientName { get; set; }
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
