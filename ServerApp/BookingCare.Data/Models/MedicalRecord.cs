using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingCare.Data.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }

        [StringLength(1000)]
        public string Diagnosis { get; set; } // Chẩn đoán

        [StringLength(2000)]
        public string Prescription { get; set; } // Đơn thuốc

        [StringLength(2000)]
        public string Notes { get; set; } // Ghi chú

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Thời gian tạo

        public DateTime? UpdatedAt { get; set; } // Thời gian cập nhật (nullable)

        public int CreatedBy { get; set; } // ID của bác sĩ tạo hồ sơ

        [ForeignKey("CreatedBy")]
        public Doctor CreatedByDoctor { get; set; }
    }
}
