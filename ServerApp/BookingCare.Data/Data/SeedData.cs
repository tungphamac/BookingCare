using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                    PasswordHash = "AQAAAAIAAYagAAAAEGXs6jlQ0fcRlYEIS79CtCpXBe5FWbJ1DrY8WfEl1rfeWk1uYzS31Z26uSCKTdTLzg==",
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
                    PasswordHash = "AQAAAAIAAYagAAAAEBdqsT6m3NC+7WLrfiDuhbO8Q1erkBM5/mIXYTD6dXiY8IgOtLVSkgIwLQkKB/si3A==",
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
                    PasswordHash = "AQAAAAIAAYagAAAAEBdqsT6m3NC+7WLrfiDuhbO8Q1erkBM5/mIXYTD6dXiY8IgOtLVSkgIwLQkKB/si3A==",
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
                    PasswordHash = "AQAAAAIAAYagAAAAELwmoQmUgF0dngxTkAHHQx1B/cyP4U3Af+3eekWM6ZLLVSHH9oFjnvZB653+9EsaMg==",
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
                }
            );

            // Seed Clinics
            modelBuilder.Entity<Clinic>().HasData(
                new Clinic
                {
                    Id = 1,
                    Name = "City Clinic",
                    Phone = 1234567890, // Lưu ý: Phone là int, nên không cần dấu "-"
                    Address = "789 Clinic St",
                    Introduction = "Top clinic in the city",
                    CreateAt = new DateTime(2025, 3, 20, 12, 0, 0),
                },
                new Clinic
                {
                    Id = 2,
                    Name = "Health Center",
                    Phone = 987654321,
                    Address = "456 Health St",
                    Introduction = "Comprehensive care",
                    CreateAt = new DateTime(2025, 3, 20, 12, 0, 0),
                }
            );

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    UserId = 2,
                    Achievement = "Best Doctor 2023",
                    Description = "Experienced cardiologist",
                    SpecializationId = 1, // Thuộc Specialization "Cardiology"
                    ClinicId = 1 // Thuộc Clinic "City Clinic"
                },
                new Doctor
                {
                    UserId = 3,
                    Achievement = "Top Neurologist 2023",
                    Description = "Expert in brain disorders",
                    SpecializationId = 2, // Thuộc Specialization "Neurology"
                    ClinicId = 2 // Thuộc Clinic "Health Center"
                }
            );

            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    UserId = 4,
                    MedicalRecordId = 1 // Sẽ liên kết với MedicalRecord sau
                }
            );

            // Seed Schedules
            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    Id = 1,
                    DoctorId = 2,
                    TimeSlot = "10:00-11:00",
                    WorkDate = new DateTime(2025, 3, 20, 12, 0, 0),
                    Status = ScheduleStatus.Available
                },
                new Schedule
                {
                    Id = 2,
                    DoctorId = 3,
                    TimeSlot = "14:00-15:00",
                    WorkDate = new DateTime(2025, 3, 20, 12, 0, 0),
                    Status = ScheduleStatus.Available
                }
            );

            // Seed Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    Date = new DateTime(2025, 3, 20, 12, 0, 0),
                    Time = new TimeSpan(10, 0, 0), // 10:00:00
                    Status = "Scheduled",
                    Reason = "Checkup for heart condition",
                    DoctorId = 2,
                    PatientId = 4,
                    ScheduleId = 1,
                    ClinicId = 1
                }
            );

            // Seed MedicalRecords
            modelBuilder.Entity<MedicalRecord>().HasData(
                new MedicalRecord
                {
                    Id = 1,
                    AppointmentId = 1
                }
            );

            // Seed Feedback
            modelBuilder.Entity<Feedback>().HasData(
                new Feedback
                {
                    Id = 1,
                    AppointmentId = 1,
                    Rating = 5,
                    Comment = "Great service!"
                }
                );
        }
    }
}
