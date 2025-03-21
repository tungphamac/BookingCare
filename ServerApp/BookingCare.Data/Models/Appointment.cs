using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace BookingCare.Data.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [StringLength(1000)]
        public string Reason { get; set; }

        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public Feedback Feedback { get; set; }

        public MedicalRecord MedicalRecord { get; set; }



    }
}
