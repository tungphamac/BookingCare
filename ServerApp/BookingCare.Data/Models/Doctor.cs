
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingCare.Data.Models
{
    public class Doctor
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }

        public string Achievement { get; set; }
        public string Description { get; set; }

        // FK: Mỗi Doctor có một Specialization
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }


        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }


        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        // 1 Doctor có nhiều Appointments
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
