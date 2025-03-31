namespace BookingCare.Business.ViewModels
{
    public class TopRatingDoctorVm
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Description { get; set; }
        public string Achievement { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}
