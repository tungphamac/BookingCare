using BookingCare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<int> AddMedicalRecordAsync(MedicalRecord record, int doctorId);
        Task<bool> UpdateMedicalRecordAsync(MedicalRecord record, int doctorId);
        Task<MedicalRecord?> ViewMedicalRecordAsync(int recordId, int userId);
    }
}
