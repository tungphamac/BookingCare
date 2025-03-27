using System.ComponentModel.DataAnnotations.Schema;

namespace BookingCare.Data.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
    }
}
