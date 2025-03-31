using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Dtos
{
    public class MedicalRecordDTO
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
    }

    public class MedicalRecordCreateDto
    {
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
    }

    public class MedicalRecordUpdateDto
    {
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
    }
}
