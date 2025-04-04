﻿using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingCare.Data.Data
{
    public class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "Doctor", NormalizedName = "DOCTOR" },
                new IdentityRole<int> { Id = 3, Name = "Patient", NormalizedName = "PATIENT" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin1@example.com",
                    NormalizedUserName = "ADMIN1@EXAMPLE.COM",
                    Email = "admin1@example.com",
                    NormalizedEmail = "ADMIN1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp1",
                    ConcurrencyStamp = "concurrency1",
                    Gender = true,
                    Address = "123 Admin St",
                    Avatar = "admin1.jpg"
                },
                new User
                {
                    Id = 2,
                    UserName = "doctor1@example.com",
                    NormalizedUserName = "DOCTOR1@EXAMPLE.COM",
                    Email = "doctor1@example.com",
                    NormalizedEmail = "DOCTOR1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp2",
                    ConcurrencyStamp = "concurrency2",
                    Gender = true,
                    Address = "123 Doctor St",
                    Avatar = "doctor1.jpg"
                },
                new User
                {
                    Id = 3,
                    UserName = "doctor2@example.com",
                    NormalizedUserName = "DOCTOR2@EXAMPLE.COM",
                    Email = "doctor2@example.com",
                    NormalizedEmail = "DOCTOR2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp3",
                    ConcurrencyStamp = "concurrency3",
                    Gender = false,
                    Address = "456 Doctor St",
                    Avatar = "doctor2.jpg"
                },
                new User
                {
                    Id = 4,
                    UserName = "patient1@example.com",
                    NormalizedUserName = "PATIENT1@EXAMPLE.COM",
                    Email = "patient1@example.com",
                    NormalizedEmail = "PATIENT1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp4",
                    ConcurrencyStamp = "concurrency4",
                    Gender = false,
                    Address = "456 Patient St",
                    Avatar = "patient1.jpg"
                }
            );

            // Seed UserRoles (Gán role cho user)
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 3, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 4, RoleId = 3 }
            );

            // Seed Specializations
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization
                {
                    Id = 1,
                    Name = "Cardiology",
                    Description = "Heart specialist",
                    Image = "cardio.jpg"
                },
                new Specialization
                {
                    Id = 2,
                    Name = "Neurology",
                    Description = "Brain specialist",
                    Image = "neuro.jpg"
                },
                new Specialization
                {
                    Id = 3,
                    Name = "Pediatrics",
                    Description = "Child health specialist",
                    Image = "pediatrics.jpg"
                },
                new Specialization
                {
                    Id = 4,
                    Name = "Orthopedics",
                    Description = "Bone and joint specialist",
                    Image = "ortho.jpg"
                },
                new Specialization
                {
                    Id = 5,
                    Name = "Dermatology",
                    Description = "Skin specialist",
                    Image = "derm.jpg"
                }
            );

            // Seed Clinics
            modelBuilder.Entity<Clinic>().HasData(
                new Clinic
                {
                    Id = 1,
                    Name = "City Clinic",
                    Phone = 1234567890,
                    Address = "789 Clinic St",
                    Introduction = "Top clinic in the city",
                    CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc)
                },
                new Clinic
                {
                    Id = 2,
                    Name = "Health Center",
                    Phone = 1234567894,
                    Address = "456 Health St",
                    Introduction = "Comprehensive care",
                    CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc)
                },
                new Clinic
                {
                    Id = 3,
                    Name = "Sunshine Clinic",
                    Phone = 1551112233,
                    Address = "123 Sunshine Ave",
                    Introduction = "Family healthcare provider",
                    CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc)
                },
                new Clinic
                {
                    Id = 4,
                    Name = "Green Valley Hospital",
                    Phone =  1442223344,
                    Address = "789 Green Valley Rd",
                    Introduction = "Specialized medical services",
                    CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc)
                },
                new Clinic
                {
                    Id = 5,
                    Name = "Blue Sky Clinic",
                    Phone = 1334445566,
                    Address = "456 Blue Sky Ln",
                    Introduction = "Modern healthcare solutions",
                    CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed quan hệ nhiều-nhiều giữa Clinic và Specialization
            modelBuilder.Entity("ClinicSpecialization").HasData(
                // City Clinic (Id = 1)
                new
                {
                    ClinicsId = 1, // City Clinic
                    SpecializationsId = 1 // Cardiology
                },
                new
                {
                    ClinicsId = 1, // City Clinic
                    SpecializationsId = 2 // Neurology
                },
                new
                {
                    ClinicsId = 1, // City Clinic
                    SpecializationsId = 3 // Pediatrics
                },
                // Health Center (Id = 2)
                new
                {
                    ClinicsId = 2, // Health Center
                    SpecializationsId = 2 // Neurology
                },
                new
                {
                    ClinicsId = 2, // Health Center
                    SpecializationsId = 4 // Orthopedics
                },
                // Sunshine Clinic (Id = 3)
                new
                {
                    ClinicsId = 3, // Sunshine Clinic
                    SpecializationsId = 3 // Pediatrics
                },
                new
                {
                    ClinicsId = 3, // Sunshine Clinic
                    SpecializationsId = 5 // Dermatology
                },
                // Green Valley Hospital (Id = 4)
                new
                {
                    ClinicsId = 4, // Green Valley Hospital
                    SpecializationsId = 1 // Cardiology
                },
                new
                {
                    ClinicsId = 4, // Green Valley Hospital
                    SpecializationsId = 4 // Orthopedics
                },
                new
                {
                    ClinicsId = 4, // Green Valley Hospital
                    SpecializationsId = 5 // Dermatology
                },
                // Blue Sky Clinic (Id = 5)
                new
                {
                    ClinicsId = 5, // Blue Sky Clinic
                    SpecializationsId = 2 // Neurology
                },
                new
                {
                    ClinicsId = 5, // Blue Sky Clinic
                    SpecializationsId = 3 // Pediatrics
                },
                new
                {
                    ClinicsId = 5, // Blue Sky Clinic
                    SpecializationsId = 5 // Dermatology
                }
            );

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    UserId = 2,
                    Achievement = "Best Doctor 2023",
                    Description = "Experienced cardiologist",
                    SpecializationId = 1,
                    ClinicId = 1
                },
                new Doctor
                {
                    UserId = 3,
                    Achievement = "Top Neurologist 2023",
                    Description = "Expert in brain disorders",
                    SpecializationId = 2,
                    ClinicId = 2
                }
            );

            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    UserId = 4,
                    MedicalRecordId = 1
                }
            );

            // Seed Schedules
            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    Id = 1,
                    DoctorId = 2,
                    TimeSlot = "10:00-11:00",
                    WorkDate = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    Status = ScheduleStatus.Available,
                    Time = new DateTime(2025, 3, 20, 10, 0, 0, DateTimeKind.Utc)
                },
                new Schedule
                {
                    Id = 2,
                    DoctorId = 3,
                    TimeSlot = "14:00-15:00",
                    WorkDate = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    Status = ScheduleStatus.Available,
                    Time = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc)
                },
                new Schedule
                {
                    Id = 3,
                    DoctorId = 3,
                    TimeSlot = "15:00-16:00",
                    WorkDate = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    Status = ScheduleStatus.Available,
                    Time = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc)
                },
                new Schedule
                {
                    Id = 4,
                    DoctorId = 2,
                    TimeSlot = "15:00-16:00",
                    WorkDate = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    Status = ScheduleStatus.Available,
                    Time = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc)

                }
            );

            // Seed Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    Date = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    Time = new TimeSpan(10, 0, 0),
                    Status = AppointmentStatus.Confirmed,
                    Reason = "Checkup for heart condition",
                    DoctorId = 2,
                    PatientId = 4,
                    ScheduleId = 1,
                    ClinicId = 1,
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed MedicalRecords
            modelBuilder.Entity<MedicalRecord>().HasData(
                new MedicalRecord
                {
                    Id = 1,
                    AppointmentId = 1,
                    Diagnosis = "Cảm cúm thông thường",
                    Prescription = "Paracetamol 500mg, uống 2 lần/ngày",
                    Notes = "Nghỉ ngơi nhiều, uống đủ nước",
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    CreatedBy = 2
                }
            );

            // Seed Feedback
            //modelBuilder.Entity<Feedback>().HasData(
            //    new Feedback
            //    {
            //        Id = 1,
            //        AppointmentId = 1,
            //        Rating = 5,
            //        Comment = "Great service!",
            //        CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) 
            //    }
            //);

            // Seed Notifications
            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    Id = 1,
                    UserId = 4,
                    Message = "Bạn có lịch hẹn mới vào ngày 20/03/2025.",
                    AppointmentId = 1,
                    IsRead = false,
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc)
                },
                new Notification
                {
                    Id = 2,
                    UserId = 2,
                    Message = "Bệnh nhân patient1@example.com đã đặt lịch hẹn vào ngày 20/03/2025.",
                    AppointmentId = 1,
                    IsRead = false,
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}