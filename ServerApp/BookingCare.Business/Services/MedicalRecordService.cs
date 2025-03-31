using BookingCare.Business.Services.Base;
using BookingCare.Business.Services.Interfaces;
using BookingCare.Data.Infrastructure;
using BookingCare.Data.Models;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
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
<<<<<<< HEAD
            if (appointment == null)
                throw new Exception("Appointment not found");

            if (appointment.DoctorId != doctorId)
                throw new Exception("Only assigned doctor can add medical record");

            // Check Record đã tồn tại
=======
            if (appointment?.DoctorId != doctorId)
                throw new Exception("Only assigned doctor can add medical record");

            // Check for existing record
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
            var existingRecord = await _unitOfWork.MedicalRecordRepository
                .GetSingleAsync(m => m.AppointmentId == record.AppointmentId);
            if (existingRecord != null)
                throw new Exception("A medical record already exists for this appointment");

<<<<<<< HEAD
            record.CreatedBy = doctorId;
            record.CreatedAt = DateTime.UtcNow;

            await AddAsync(record);
            return record.Id;
=======
            return await AddAsync(record);
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
        }

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecord record, int doctorId)
        {
            var existing = await GetByIdAsync(record.Id);
            if (existing == null)
                return false;

            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(existing.AppointmentId);
<<<<<<< HEAD
            if (appointment == null)
                throw new Exception("Appointment not found");

            if (appointment.DoctorId != doctorId)
                throw new Exception("Only assigned doctor can update medical record");

            existing.Diagnosis = record.Diagnosis;
            existing.Prescription = record.Prescription;
            existing.Notes = record.Notes;
            existing.UpdatedAt = DateTime.UtcNow;

            return await UpdateAsync(existing);
=======
            if (appointment?.DoctorId != doctorId)
                throw new Exception("Only assigned doctor can update medical record");

            return await UpdateAsync(record);
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
        }

        public async Task<MedicalRecord?> ViewMedicalRecordAsync(int recordId, int userId)
        {
            var record = await GetByIdAsync(recordId);
            if (record == null) return null;

            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(record.AppointmentId);
<<<<<<< HEAD
            if (appointment == null)
                throw new Exception("Appointment not found");

            var isPatient = appointment.PatientId == userId;
            var doctor = await _unitOfWork.DoctorRepository.GetQuery(d => d.UserId == userId).FirstOrDefaultAsync();
            var isDoctor = doctor != null && appointment.DoctorId == doctor.UserId;
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            var isAdmin = user?.Doctor == null && user?.Patient == null;
=======
            var isPatient = appointment?.PatientId == userId;
            var isDoctor = appointment?.DoctorId == userId;
            var isAdmin = (await _unitOfWork.UserRepository.GetByIdAsync(userId))?.Doctor == null &&
                         (await _unitOfWork.UserRepository.GetByIdAsync(userId))?.Patient == null;
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06

            if (!isAdmin && !isDoctor && !isPatient)
                throw new Exception("Unauthorized access to medical record");

            return record;
        }
    }
}
