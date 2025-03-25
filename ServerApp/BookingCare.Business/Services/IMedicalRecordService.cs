using BookingCare.Data.Models;

namespace BookingCare.Business.Services
{
    public interface IMedicalRecordService
    {
        Task<int> AddMedicalRecordAsync(MedicalRecord record, int doctorId);
        Task<bool> UpdateMedicalRecordAsync(MedicalRecord record, int doctorId);
        Task<MedicalRecord?> ViewMedicalRecordAsync(int recordId, int userId);
    }
}