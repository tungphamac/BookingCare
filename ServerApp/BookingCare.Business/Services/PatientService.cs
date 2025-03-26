using BookingCare.Data.Data;
using BookingCare.Data.Models;
using BookingCare.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class PatientService
    {
        private readonly PatientRepository _repository;
        private readonly AppDbContext _context;

        public PatientService(PatientRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // Get all patients
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _repository.GetAllPatientsAsync();
        }

        // Get patient by Id
        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _repository.GetPatientByIdAsync(id);
        }

        // Add a new patient
        public async Task AddPatientAsync(Patient patient)
        {
            await _repository.AddPatientAsync(patient);
        }

        // Update patient's details
        public async Task UpdatePatientAsync(Patient patient)
        {
            await _repository.UpdatePatientAsync(patient);
        }

        // Delete a patient
        public async Task DeletePatientAsync(int id)
        {
            await _repository.DeletePatientAsync(id);
        }
    }
}
