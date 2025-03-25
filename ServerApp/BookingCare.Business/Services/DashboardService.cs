using BookingCare.Data.Data;
using BookingCare.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;

        public class DashboardService
        {
            private readonly DoctorRepository _doctorRepository;
            
            //private readonly AppointmentRepository _appointmentRepository;

            public DashboardServices(DoctorRepository doctorRepository, PatientRepository patientRepository)
            {
                _doctorRepository = doctorRepository;
               
                
            }

            // Lấy thông tin tổng quan cho dashboard
            public async Task<DashboardService> GetDashboardOverviewAsync()
            {
                var totalDoctors = await _doctorRepository.GetTotalDoctorsAsync(); // Tổng số bác sĩ
              
              
                return new Dashbo
                {
                    TotalDoctors = totalDoctors,
                   
                   
                };
            }
        }
    }
