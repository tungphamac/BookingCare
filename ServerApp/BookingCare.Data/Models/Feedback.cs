using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingCare.Data.Models
{
    public class Feedback
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
