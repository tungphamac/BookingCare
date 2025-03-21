using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingCare.Data.Models
{
    public class Patient
    {
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }

        public int MedicalRecordId { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
