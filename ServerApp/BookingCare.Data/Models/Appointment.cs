using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingCare.Data.Models
{
   
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Rejected,
    }

    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending; // Sửa thành AppointmentStatus

        [Required]
        [StringLength(1000)]
        public string Reason { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public Feedback Feedback { get; set; }

        public MedicalRecord MedicalRecord { get; set; }
    }
}