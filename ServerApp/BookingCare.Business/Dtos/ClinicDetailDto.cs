namespace BookingCare.API.Dtos
{
    public class ClinicDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Introduction { get; set; }
        public DateTime CreateAt { get; set; }
    }
   
}