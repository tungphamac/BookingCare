using BookingCare.Data.Data;
using BookingCare.Data.DTOs;
using BookingCare.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingCare.Business;

namespace BookingCare.Data.Repositories
{
    public class PatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all patients
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .Include(p => p.MedicalRecord)
                .Include(p => p.Appointments)
                .ToListAsync();
        }

        // Get patient by Id
        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Include(p => p.MedicalRecord)
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.UserId == id);
        }

        // Add a new patient
        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        // Update a patient's details
        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        // Delete a patient
        public async Task DeletePatientAsync(int id)
        {
            var patient = await GetPatientByIdAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }

}