using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class MedicalRecordService : BaseService<MedicalRecord>, IMedicalRecordService
    {
        public MedicalRecordService(ILogger<BaseService<MedicalRecord>> logger, IUnitOfWork unitOfWork)
            : base(logger, unitOfWork) { }

        public async Task<int> AddMedicalRecordAsync(MedicalRecord record, int doctorId)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(record.AppointmentId);
            if (appointment?.DoctorId != doctorId)
                throw new Exception("Only assigned doctor can add medical record");

            // Check for existing record
            var existingRecord = await _unitOfWork.MedicalRecordRepository
                .GetSingleAsync(m => m.AppointmentId == record.AppointmentId);
            if (existingRecord != null)
                throw new Exception("A medical record already exists for this appointment");

            return await AddAsync(record);
        }

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord record, int doctorId)
        {
            var existing = await GetByIdAsync(record.Id);
            if (existing == null)
                return false;

            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(existing.AppointmentId);
            if (appointment?.DoctorId != doctorId)
                throw new Exception("Only assigned doctor can update medical record");

            return await UpdateAsync(record);
        }

        public async Task<MedicalRecord?> ViewMedicalRecordAsync(int recordId, int userId)
        {
            var record = await GetByIdAsync(recordId);
            if (record == null) return null;

            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(record.AppointmentId);
            var isPatient = appointment?.PatientId == userId;
            var isDoctor = appointment?.DoctorId == userId;
            var isAdmin = (await _unitOfWork.UserRepository.GetByIdAsync(userId))?.Doctor == null &&
                         (await _unitOfWork.UserRepository.GetByIdAsync(userId))?.Patient == null;

            if (!isAdmin && !isDoctor && !isPatient)
                throw new Exception("Unauthorized access to medical record");

            return record;
        }
    }
}
