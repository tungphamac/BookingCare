using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingCare.Data.Data
{
    public class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed Roles (Giữ nguyên)
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "Doctor", NormalizedName = "DOCTOR" },
                new IdentityRole<int> { Id = 3, Name = "Patient", NormalizedName = "PATIENT" }
            );

            // Seed Users (Giữ nguyên - đã có 5 Patients và 5 Doctors mới)
            modelBuilder.Entity<User>().HasData(
                // Dữ liệu hiện có
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
                    Avatar = "admin1.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567890"
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
                    Avatar = "doctor1.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567891"
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
                    Avatar = "doctor2.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567892"
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
                    Avatar = "patient1.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567893"
                },
                // 5 Patients mới (Id từ 5 đến 9)
                new User
                {
                    Id = 5,
                    UserName = "patient2@example.com",
                    NormalizedUserName = "PATIENT2@EXAMPLE.COM",
                    Email = "patient2@example.com",
                    NormalizedEmail = "PATIENT2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp5",
                    ConcurrencyStamp = "concurrency5",
                    Gender = false,
                    Address = "101 Patient St",
                    Avatar = "patient2.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567894"
                },
                new User
                {
                    Id = 6,
                    UserName = "patient3@example.com",
                    NormalizedUserName = "PATIENT3@EXAMPLE.COM",
                    Email = "patient3@example.com",
                    NormalizedEmail = "PATIENT3@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp6",
                    ConcurrencyStamp = "concurrency6",
                    Gender = true,
                    Address = "202 Patient St",
                    Avatar = "patient3.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567895"
                },
                new User
                {
                    Id = 7,
                    UserName = "patient4@example.com",
                    NormalizedUserName = "PATIENT4@EXAMPLE.COM",
                    Email = "patient4@example.com",
                    NormalizedEmail = "PATIENT4@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp7",
                    ConcurrencyStamp = "concurrency7",
                    Gender = false,
                    Address = "303 Patient St",
                    Avatar = "patient4.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567896"
                },
                new User
                {
                    Id = 8,
                    UserName = "patient5@example.com",
                    NormalizedUserName = "PATIENT5@EXAMPLE.COM",
                    Email = "patient5@example.com",
                    NormalizedEmail = "PATIENT5@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp8",
                    ConcurrencyStamp = "concurrency8",
                    Gender = true,
                    Address = "404 Patient St",
                    Avatar = "patient5.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567897"
                },
                new User
                {
                    Id = 9,
                    UserName = "patient6@example.com",
                    NormalizedUserName = "PATIENT6@EXAMPLE.COM",
                    Email = "patient6@example.com",
                    NormalizedEmail = "PATIENT6@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp9",
                    ConcurrencyStamp = "concurrency9",
                    Gender = false,
                    Address = "505 Patient St",
                    Avatar = "patient6.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567898"
                },
                // 5 Doctors mới (Id từ 10 đến 14)
                new User
                {
                    Id = 10,
                    UserName = "doctor3@example.com",
                    NormalizedUserName = "DOCTOR3@EXAMPLE.COM",
                    Email = "doctor3@example.com",
                    NormalizedEmail = "DOCTOR3@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp10",
                    ConcurrencyStamp = "concurrency10",
                    Gender = true,
                    Address = "606 Doctor St",
                    Avatar = "doctor3.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567899"
                },
                new User
                {
                    Id = 11,
                    UserName = "doctor4@example.com",
                    NormalizedUserName = "DOCTOR4@EXAMPLE.COM",
                    Email = "doctor4@example.com",
                    NormalizedEmail = "DOCTOR4@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp11",
                    ConcurrencyStamp = "concurrency11",
                    Gender = false,
                    Address = "707 Doctor St",
                    Avatar = "doctor4.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567800"
                },
                new User
                {
                    Id = 12,
                    UserName = "doctor5@example.com",
                    NormalizedUserName = "DOCTOR5@EXAMPLE.COM",
                    Email = "doctor5@example.com",
                    NormalizedEmail = "DOCTOR5@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp12",
                    ConcurrencyStamp = "concurrency12",
                    Gender = true,
                    Address = "808 Doctor St",
                    Avatar = "doctor5.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567801"
                },
                new User
                {
                    Id = 13,
                    UserName = "doctor6@example.com",
                    NormalizedUserName = "DOCTOR6@EXAMPLE.COM",
                    Email = "doctor6@example.com",
                    NormalizedEmail = "DOCTOR6@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp13",
                    ConcurrencyStamp = "concurrency13",
                    Gender = false,
                    Address = "909 Doctor St",
                    Avatar = "doctor6.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567802"
                },
                new User
                {
                    Id = 14,
                    UserName = "doctor7@example.com",
                    NormalizedUserName = "DOCTOR7@EXAMPLE.COM",
                    Email = "doctor7@example.com",
                    NormalizedEmail = "DOCTOR7@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAE...hashedpassword...",
                    SecurityStamp = "stamp14",
                    ConcurrencyStamp = "concurrency14",
                    Gender = true,
                    Address = "1010 Doctor St",
                    Avatar = "doctor7.jpg",
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    PhoneNumber = "1234567803"
                }
            );

            // Seed UserRoles (Giữ nguyên - đã có role cho các user mới)
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 3, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 4, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 5, RoleId = 3 }, // patient2
                new IdentityUserRole<int> { UserId = 6, RoleId = 3 }, // patient3
                new IdentityUserRole<int> { UserId = 7, RoleId = 3 }, // patient4
                new IdentityUserRole<int> { UserId = 8, RoleId = 3 }, // patient5
                new IdentityUserRole<int> { UserId = 9, RoleId = 3 }, // patient6
                new IdentityUserRole<int> { UserId = 10, RoleId = 2 }, // doctor3
                new IdentityUserRole<int> { UserId = 11, RoleId = 2 }, // doctor4
                new IdentityUserRole<int> { UserId = 12, RoleId = 2 }, // doctor5
                new IdentityUserRole<int> { UserId = 13, RoleId = 2 }, // doctor6
                new IdentityUserRole<int> { UserId = 14, RoleId = 2 }  // doctor7
            );

            // Seed Specializations (Thêm 20 bản ghi mới, Id từ 6 đến 25)
            modelBuilder.Entity<Specialization>().HasData(
                // Dữ liệu hiện có
                new Specialization { Id = 1, Name = "Cardiology", Description = "Heart specialist", Image = "cardio.jpg" },
                new Specialization { Id = 2, Name = "Neurology", Description = "Brain specialist", Image = "neuro.jpg" },
                new Specialization { Id = 3, Name = "Pediatrics", Description = "Child health specialist", Image = "pediatrics.jpg" },
                new Specialization { Id = 4, Name = "Orthopedics", Description = "Bone and joint specialist", Image = "ortho.jpg" },
                new Specialization { Id = 5, Name = "Dermatology", Description = "Skin specialist", Image = "derm.jpg" },
                // Thêm 20 Specialization mới
                new Specialization { Id = 6, Name = "Oncology", Description = "Cancer specialist", Image = "onco.jpg" },
                new Specialization { Id = 7, Name = "Gastroenterology", Description = "Digestive system specialist", Image = "gastro.jpg" },
                new Specialization { Id = 8, Name = "Endocrinology", Description = "Hormone specialist", Image = "endo.jpg" },
                new Specialization { Id = 9, Name = "Psychiatry", Description = "Mental health specialist", Image = "psych.jpg" },
                new Specialization { Id = 10, Name = "Ophthalmology", Description = "Eye specialist", Image = "ophthal.jpg" },
                new Specialization { Id = 11, Name = "Urology", Description = "Urinary system specialist", Image = "uro.jpg" },
                new Specialization { Id = 12, Name = "Gynecology", Description = "Women’s health specialist", Image = "gyn.jpg" },
                new Specialization { Id = 13, Name = "Rheumatology", Description = "Joint and autoimmune specialist", Image = "rheum.jpg" },
                new Specialization { Id = 14, Name = "Pulmonology", Description = "Lung specialist", Image = "pulmo.jpg" },
                new Specialization { Id = 15, Name = "Nephrology", Description = "Kidney specialist", Image = "nephro.jpg" },
                new Specialization { Id = 16, Name = "Hematology", Description = "Blood specialist", Image = "hema.jpg" },
                new Specialization { Id = 17, Name = "Allergy", Description = "Allergy specialist", Image = "allergy.jpg" },
                new Specialization { Id = 18, Name = "Infectious Disease", Description = "Infectious disease specialist", Image = "infect.jpg" },
                new Specialization { Id = 19, Name = "Plastic Surgery", Description = "Cosmetic surgery specialist", Image = "plastic.jpg" },
                new Specialization { Id = 20, Name = "Anesthesiology", Description = "Anesthesia specialist", Image = "anes.jpg" },
                new Specialization { Id = 21, Name = "Radiology", Description = "Imaging specialist", Image = "radio.jpg" },
                new Specialization { Id = 22, Name = "Pathology", Description = "Disease diagnosis specialist", Image = "patho.jpg" },
                new Specialization { Id = 23, Name = "Geriatrics", Description = "Elderly care specialist", Image = "geri.jpg" },
                new Specialization { Id = 24, Name = "Sports Medicine", Description = "Sports injury specialist", Image = "sports.jpg" },
                new Specialization { Id = 25, Name = "Emergency Medicine", Description = "Emergency care specialist", Image = "emerg.jpg" }
            );

            // Seed Clinics (Thêm 20 bản ghi mới, Id từ 6 đến 25)
            modelBuilder.Entity<Clinic>().HasData(
                // Dữ liệu hiện có
                new Clinic { Id = 1, Name = "City Clinic", Phone = 1234567890, Address = "789 Clinic St", Introduction = "Top clinic in the city", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 2, Name = "Health Center", Phone = 1234567894, Address = "456 Health St", Introduction = "Comprehensive care", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 3, Name = "Sunshine Clinic", Phone = 1551112233, Address = "123 Sunshine Ave", Introduction = "Family healthcare provider", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 4, Name = "Green Valley Hospital", Phone = 1442223344, Address = "789 Green Valley Rd", Introduction = "Specialized medical services", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 5, Name = "Blue Sky Clinic", Phone = 1334445566, Address = "456 Blue Sky Ln", Introduction = "Modern healthcare solutions", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                // Thêm 20 Clinic mới
                new Clinic { Id = 6, Name = "Hope Clinic", Phone = 1234567801, Address = "111 Hope St", Introduction = "Quality care for all", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 7, Name = "Life Hospital", Phone = 1234567802, Address = "222 Life Rd", Introduction = "Advanced medical services", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 8, Name = "Peace Center", Phone = 1234567803, Address = "333 Peace Ave", Introduction = "Holistic healthcare", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 9, Name = "Care Clinic", Phone = 1234567804, Address = "444 Care Ln", Introduction = "Patient-centered care", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 10, Name = "Wellness Hub", Phone = 1234567805, Address = "555 Wellness Dr", Introduction = "Promoting wellness", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 11, Name = "Harmony Clinic", Phone = 1234567806, Address = "666 Harmony St", Introduction = "Balanced healthcare", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 12, Name = "Vitality Center", Phone = 1234567807, Address = "777 Vitality Rd", Introduction = "Vital health solutions", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 13, Name = "Unity Hospital", Phone = 1234567808, Address = "888 Unity Ave", Introduction = "Unified care approach", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 14, Name = "Bright Clinic", Phone = 1234567809, Address = "999 Bright Ln", Introduction = "Brightening lives", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 15, Name = "Healing Point", Phone = 1234567810, Address = "101 Healing Dr", Introduction = "Healing for all", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 16, Name = "Evergreen Clinic", Phone = 1234567811, Address = "222 Evergreen St", Introduction = "Everlasting care", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 17, Name = "Star Hospital", Phone = 1234567812, Address = "333 Star Rd", Introduction = "Shining health services", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 18, Name = "Golden Care", Phone = 1234567813, Address = "444 Golden Ave", Introduction = "Golden standard care", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 19, Name = "Silver Clinic", Phone = 1234567814, Address = "555 Silver Ln", Introduction = "Silver quality care", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 20, Name = "Platinum Center", Phone = 1234567815, Address = "666 Platinum Dr", Introduction = "Platinum healthcare", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 21, Name = "Diamond Clinic", Phone = 1234567816, Address = "777 Diamond St", Introduction = "Diamond care services", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 22, Name = "Ruby Hospital", Phone = 1234567817, Address = "888 Ruby Rd", Introduction = "Ruby health solutions", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 23, Name = "Sapphire Clinic", Phone = 1234567818, Address = "999 Sapphire Ave", Introduction = "Sapphire care provider", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 24, Name = "Emerald Center", Phone = 1234567819, Address = "1010 Emerald Ln", Introduction = "Emerald healthcare", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Clinic { Id = 25, Name = "Opal Clinic", Phone = 1234567820, Address = "1111 Opal Dr", Introduction = "Opal care solutions", CreateAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) }
            );

            // Seed quan hệ nhiều-nhiều giữa Clinic và Specialization (Thêm 20 bản ghi mới)
            modelBuilder.Entity("ClinicSpecialization").HasData(
                // Dữ liệu hiện có
                new { ClinicsId = 1, SpecializationsId = 1 },
                new { ClinicsId = 1, SpecializationsId = 2 },
                new { ClinicsId = 1, SpecializationsId = 3 },
                new { ClinicsId = 2, SpecializationsId = 2 },
                new { ClinicsId = 2, SpecializationsId = 4 },
                new { ClinicsId = 3, SpecializationsId = 3 },
                new { ClinicsId = 3, SpecializationsId = 5 },
                new { ClinicsId = 4, SpecializationsId = 1 },
                new { ClinicsId = 4, SpecializationsId = 4 },
                new { ClinicsId = 4, SpecializationsId = 5 },
                new { ClinicsId = 5, SpecializationsId = 2 },
                new { ClinicsId = 5, SpecializationsId = 3 },
                new { ClinicsId = 5, SpecializationsId = 5 },
                // Thêm 20 quan hệ mới (cho các Clinic Id từ 6 đến 25)
                new { ClinicsId = 6, SpecializationsId = 6 },
                new { ClinicsId = 7, SpecializationsId = 7 },
                new { ClinicsId = 8, SpecializationsId = 8 },
                new { ClinicsId = 9, SpecializationsId = 9 },
                new { ClinicsId = 10, SpecializationsId = 10 },
                new { ClinicsId = 11, SpecializationsId = 11 },
                new { ClinicsId = 12, SpecializationsId = 12 },
                new { ClinicsId = 13, SpecializationsId = 13 },
                new { ClinicsId = 14, SpecializationsId = 14 },
                new { ClinicsId = 15, SpecializationsId = 15 },
                new { ClinicsId = 16, SpecializationsId = 16 },
                new { ClinicsId = 17, SpecializationsId = 17 },
                new { ClinicsId = 18, SpecializationsId = 18 },
                new { ClinicsId = 19, SpecializationsId = 19 },
                new { ClinicsId = 20, SpecializationsId = 20 },
                new { ClinicsId = 21, SpecializationsId = 21 },
                new { ClinicsId = 22, SpecializationsId = 22 },
                new { ClinicsId = 23, SpecializationsId = 23 },
                new { ClinicsId = 24, SpecializationsId = 24 },
                new { ClinicsId = 25, SpecializationsId = 25 }
            );

            // Seed MedicalRecords (Giữ nguyên - đã có 5 bản ghi mới)
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
                },
                new MedicalRecord
                {
                    Id = 2,
                    AppointmentId = 2,
                    Diagnosis = "Hypertension",
                    Prescription = "Lisinopril 10mg",
                    Notes = "Monitor blood pressure",
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    CreatedBy = 2
                },
                new MedicalRecord
                {
                    Id = 3,
                    AppointmentId = 3,
                    Diagnosis = "Diabetes",
                    Prescription = "Metformin 500mg",
                    Notes = "Check blood sugar",
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    CreatedBy = 2
                },
                new MedicalRecord
                {
                    Id = 4,
                    AppointmentId = 4,
                    Diagnosis = "Allergy",
                    Prescription = "Antihistamine",
                    Notes = "Avoid allergens",
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    CreatedBy = 2
                },
                new MedicalRecord
                {
                    Id = 5,
                    AppointmentId = 5,
                    Diagnosis = "Flu",
                    Prescription = "Rest and fluids",
                    Notes = "Stay hydrated",
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    CreatedBy = 2
                },
                new MedicalRecord
                {
                    Id = 6,
                    AppointmentId = 6,
                    Diagnosis = "Migraine",
                    Prescription = "Ibuprofen 200mg",
                    Notes = "Rest in dark room",
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc),
                    CreatedBy = 2
                }
            );

            // Seed Doctors (Giữ nguyên - đã có 5 bác sĩ mới)
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { UserId = 2, Achievement = "Best Doctor 2023", Description = "Experienced cardiologist", SpecializationId = 1, ClinicId = 1 },
                new Doctor { UserId = 3, Achievement = "Top Neurologist 2023", Description = "Expert in brain disorders", SpecializationId = 2, ClinicId = 2 },
                new Doctor { UserId = 10, Achievement = "Oncology Expert 2024", Description = "Cancer specialist", SpecializationId = 1, ClinicId = 1 },
                new Doctor { UserId = 11, Achievement = "Neuro Award 2024", Description = "Brain expert", SpecializationId = 2, ClinicId = 2 },
                new Doctor { UserId = 12, Achievement = "Pediatric Star 2024", Description = "Child health expert", SpecializationId = 3, ClinicId = 3 },
                new Doctor { UserId = 13, Achievement = "Ortho Innovator 2024", Description = "Bone specialist", SpecializationId = 4, ClinicId = 4 },
                new Doctor { UserId = 14, Achievement = "Derm Leader 2024", Description = "Skin care expert", SpecializationId = 5, ClinicId = 5 }
            );

            // Seed Patients (Giữ nguyên - đã có 5 bệnh nhân mới)
            modelBuilder.Entity<Patient>().HasData(
                new Patient { UserId = 4, MedicalRecordId = 1 },
                new Patient { UserId = 5, MedicalRecordId = 2 },
                new Patient { UserId = 6, MedicalRecordId = 3 },
                new Patient { UserId = 7, MedicalRecordId = 4 },
                new Patient { UserId = 8, MedicalRecordId = 5 },
                new Patient { UserId = 9, MedicalRecordId = 6 }
            );

            // Seed Schedules (Thêm 20 bản ghi mới, Id từ 5 đến 24)
            modelBuilder.Entity<Schedule>().HasData(
                // Dữ liệu hiện có
                new Schedule { Id = 1, DoctorId = 2, TimeSlot = "10:00-11:00", WorkDate = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 20, 10, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 2, DoctorId = 3, TimeSlot = "14:00-15:00", WorkDate = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 3, DoctorId = 3, TimeSlot = "15:00-16:00", WorkDate = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 4, DoctorId = 2, TimeSlot = "15:00-16:00", WorkDate = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc) },
                // Thêm 20 Schedule mới (Id từ 5 đến 24)
                new Schedule { Id = 5, DoctorId = 10, TimeSlot = "08:00-09:00", WorkDate = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 21, 8, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 6, DoctorId = 10, TimeSlot = "09:00-10:00", WorkDate = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 21, 9, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 7, DoctorId = 11, TimeSlot = "10:00-11:00", WorkDate = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 21, 10, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 8, DoctorId = 11, TimeSlot = "11:00-12:00", WorkDate = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 21, 11, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 9, DoctorId = 12, TimeSlot = "13:00-14:00", WorkDate = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 21, 13, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 10, DoctorId = 12, TimeSlot = "14:00-15:00", WorkDate = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 21, 14, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 11, DoctorId = 13, TimeSlot = "15:00-16:00", WorkDate = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 21, 15, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 12, DoctorId = 13, TimeSlot = "16:00-17:00", WorkDate = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 21, 16, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 13, DoctorId = 14, TimeSlot = "08:00-09:00", WorkDate = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 22, 8, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 14, DoctorId = 14, TimeSlot = "09:00-10:00", WorkDate = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 22, 9, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 15, DoctorId = 2, TimeSlot = "10:00-11:00", WorkDate = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 22, 10, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 16, DoctorId = 2, TimeSlot = "11:00-12:00", WorkDate = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 22, 11, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 17, DoctorId = 3, TimeSlot = "13:00-14:00", WorkDate = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 22, 13, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 18, DoctorId = 3, TimeSlot = "14:00-15:00", WorkDate = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 22, 14, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 19, DoctorId = 10, TimeSlot = "15:00-16:00", WorkDate = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 22, 15, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 20, DoctorId = 10, TimeSlot = "16:00-17:00", WorkDate = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 22, 16, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 21, DoctorId = 11, TimeSlot = "08:00-09:00", WorkDate = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 23, 8, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 22, DoctorId = 11, TimeSlot = "09:00-10:00", WorkDate = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 23, 9, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 23, DoctorId = 12, TimeSlot = "10:00-11:00", WorkDate = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 23, 10, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 24, DoctorId = 12, TimeSlot = "11:00-12:00", WorkDate = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc), Status = ScheduleStatus.Available, Time = new DateTime(2025, 3, 23, 11, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Appointments (Thêm 20 bản ghi mới, Id từ 2 đến 21)
            modelBuilder.Entity<Appointment>().HasData(
                // Dữ liệu hiện có
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
                },
                // Thêm 20 Appointment mới
                new Appointment { Id = 2, Date = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(8, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Cancer screening", DoctorId = 10, PatientId = 5, ScheduleId = 5, ClinicId = 1, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 3, Date = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Follow-up cancer", DoctorId = 10, PatientId = 6, ScheduleId = 6, ClinicId = 1, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 4, Date = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(10, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Headache check", DoctorId = 11, PatientId = 7, ScheduleId = 7, ClinicId = 2, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 5, Date = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(11, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Neurological exam", DoctorId = 11, PatientId = 8, ScheduleId = 8, ClinicId = 2, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 6, Date = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(13, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Child checkup", DoctorId = 12, PatientId = 9, ScheduleId = 9, ClinicId = 3, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 7, Date = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(14, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Pediatric follow-up", DoctorId = 12, PatientId = 4, ScheduleId = 10, ClinicId = 3, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 8, Date = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(15, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Bone pain", DoctorId = 13, PatientId = 5, ScheduleId = 11, ClinicId = 4, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 9, Date = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(16, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Joint stiffness", DoctorId = 13, PatientId = 6, ScheduleId = 12, ClinicId = 4, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 10, Date = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(8, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Skin rash", DoctorId = 14, PatientId = 7, ScheduleId = 13, ClinicId = 5, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 11, Date = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Dermatology consult", DoctorId = 14, PatientId = 8, ScheduleId = 14, ClinicId = 5, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 12, Date = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(10, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Heart checkup", DoctorId = 2, PatientId = 9, ScheduleId = 15, ClinicId = 1, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 13, Date = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(11, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Chest pain", DoctorId = 2, PatientId = 4, ScheduleId = 16, ClinicId = 1, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 14, Date = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(13, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Brain scan follow-up", DoctorId = 3, PatientId = 5, ScheduleId = 17, ClinicId = 2, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 15, Date = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(14, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Neurology consult", DoctorId = 3, PatientId = 6, ScheduleId = 18, ClinicId = 2, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 16, Date = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(15, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Oncology follow-up", DoctorId = 10, PatientId = 7, ScheduleId = 19, ClinicId = 1, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 17, Date = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(16, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Cancer treatment plan", DoctorId = 10, PatientId = 8, ScheduleId = 20, ClinicId = 1, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 18, Date = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(8, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Neurology exam", DoctorId = 11, PatientId = 9, ScheduleId = 21, ClinicId = 2, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 19, Date = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Head injury check", DoctorId = 11, PatientId = 4, ScheduleId = 22, ClinicId = 2, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 20, Date = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(10, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Child vaccination", DoctorId = 12, PatientId = 5, ScheduleId = 23, ClinicId = 3, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Appointment { Id = 21, Date = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc), Time = new TimeSpan(11, 0, 0), Status = AppointmentStatus.Confirmed, Reason = "Pediatric checkup", DoctorId = 12, PatientId = 6, ScheduleId = 24, ClinicId = 3, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Feedback (Thêm 20 bản ghi mới, Id từ 1 đến 20)
            modelBuilder.Entity<Feedback>().HasData(
                // Bỏ comment và thêm dữ liệu
                new Feedback { Id = 1, AppointmentId = 1, Rating = 5, Comment = "Great service!", CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                // Thêm 20 Feedback mới (Id từ 2 đến 21)
                new Feedback { Id = 2, AppointmentId = 2, Rating = 4, Comment = "Good care", CreatedAt = new DateTime(2025, 3, 21, 9, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 3, AppointmentId = 3, Rating = 5, Comment = "Excellent service", CreatedAt = new DateTime(2025, 3, 21, 10, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 4, AppointmentId = 4, Rating = 3, Comment = "Average experience", CreatedAt = new DateTime(2025, 3, 21, 11, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 5, AppointmentId = 5, Rating = 5, Comment = "Very helpful", CreatedAt = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 6, AppointmentId = 6, Rating = 4, Comment = "Friendly staff", CreatedAt = new DateTime(2025, 3, 21, 14, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 7, AppointmentId = 7, Rating = 5, Comment = "Great doctor", CreatedAt = new DateTime(2025, 3, 21, 15, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 8, AppointmentId = 8, Rating = 4, Comment = "Good visit", CreatedAt = new DateTime(2025, 3, 21, 16, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 9, AppointmentId = 9, Rating = 3, Comment = "Could improve", CreatedAt = new DateTime(2025, 3, 21, 17, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 10, AppointmentId = 10, Rating = 5, Comment = "Highly recommend", CreatedAt = new DateTime(2025, 3, 22, 9, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 11, AppointmentId = 11, Rating = 4, Comment = "Professional care", CreatedAt = new DateTime(2025, 3, 22, 10, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 12, AppointmentId = 12, Rating = 5, Comment = "Very satisfied", CreatedAt = new DateTime(2025, 3, 22, 11, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 13, AppointmentId = 13, Rating = 4, Comment = "Good experience", CreatedAt = new DateTime(2025, 3, 22, 12, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 14, AppointmentId = 14, Rating = 3, Comment = "Okay service", CreatedAt = new DateTime(2025, 3, 22, 14, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 15, AppointmentId = 15, Rating = 5, Comment = "Amazing doctor", CreatedAt = new DateTime(2025, 3, 22, 15, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 16, AppointmentId = 16, Rating = 4, Comment = "Helpful session", CreatedAt = new DateTime(2025, 3, 22, 16, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 17, AppointmentId = 17, Rating = 5, Comment = "Excellent care", CreatedAt = new DateTime(2025, 3, 22, 17, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 18, AppointmentId = 18, Rating = 4, Comment = "Good consultation", CreatedAt = new DateTime(2025, 3, 23, 9, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 19, AppointmentId = 19, Rating = 3, Comment = "Average visit", CreatedAt = new DateTime(2025, 3, 23, 10, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 20, AppointmentId = 20, Rating = 5, Comment = "Very happy", CreatedAt = new DateTime(2025, 3, 23, 11, 0, 0, DateTimeKind.Utc) },
                new Feedback { Id = 21, AppointmentId = 21, Rating = 4, Comment = "Great pediatric care", CreatedAt = new DateTime(2025, 3, 23, 12, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Notifications (Thêm 20 bản ghi mới, Id từ 3 đến 22)
            modelBuilder.Entity<Notification>().HasData(
                // Dữ liệu hiện có
                new Notification { Id = 1, UserId = 4, Message = "Bạn có lịch hẹn mới vào ngày 20/03/2025.", AppointmentId = 1, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 2, UserId = 2, Message = "Bệnh nhân patient1@example.com đã đặt lịch hẹn vào ngày 20/03/2025.", AppointmentId = 1, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                // Thêm 20 Notification mới
                new Notification { Id = 3, UserId = 5, Message = "Bạn có lịch hẹn mới vào ngày 21/03/2025.", AppointmentId = 2, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 4, UserId = 10, Message = "Bệnh nhân patient2@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", AppointmentId = 2, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 5, UserId = 6, Message = "Bạn có lịch hẹn mới vào ngày 21/03/2025.", AppointmentId = 3, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 6, UserId = 10, Message = "Bệnh nhân patient3@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", AppointmentId = 3, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 7, UserId = 7, Message = "Bạn có lịch hẹn mới vào ngày 21/03/2025.", AppointmentId = 4, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 8, UserId = 11, Message = "Bệnh nhân patient4@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", AppointmentId = 4, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 9, UserId = 8, Message = "Bạn có lịch hẹn mới vào ngày 21/03/2025.", AppointmentId = 5, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 10, UserId = 11, Message = "Bệnh nhân patient5@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", AppointmentId = 5, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 11, UserId = 9, Message = "Bạn có lịch hẹn mới vào ngày 21/03/2025.", AppointmentId = 6, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 12, UserId = 12, Message = "Bệnh nhân patient6@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", AppointmentId = 6, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 13, UserId = 4, Message = "Bạn có lịch hẹn mới vào ngày 21/03/2025.", AppointmentId = 7, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 14, UserId = 12, Message = "Bệnh nhân patient1@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", AppointmentId = 7, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 15, UserId = 5, Message = "Bạn có lịch hẹn mới vào ngày 21/03/2025.", AppointmentId = 8, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 16, UserId = 13, Message = "Bệnh nhân patient2@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", AppointmentId = 8, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 17, UserId = 6, Message = "Bạn có lịch hẹn mới vào ngày 21/03/2025.", AppointmentId = 9, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 18, UserId = 13, Message = "Bệnh nhân patient3@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", AppointmentId = 9, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 19, UserId = 7, Message = "Bạn có lịch hẹn mới vào ngày 22/03/2025.", AppointmentId = 10, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 20, UserId = 14, Message = "Bệnh nhân patient4@example.com đã đặt lịch hẹn vào ngày 22/03/2025.", AppointmentId = 10, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 21, UserId = 8, Message = "Bạn có lịch hẹn mới vào ngày 22/03/2025.", AppointmentId = 11, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) },
                new Notification { Id = 22, UserId = 14, Message = "Bệnh nhân patient5@example.com đã đặt lịch hẹn vào ngày 22/03/2025.", AppointmentId = 11, IsRead = false, CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Messages (Thêm 20 bản ghi mới, Id từ 1 đến 20)
            modelBuilder.Entity<Message>().HasData(
                // Thêm 20 Message mới (giữa Patients và Doctors)
                new Message { Id = 1, SenderId = 10, ReceiverId = 5, Content = "Please prepare for your cancer screening.", SentAt = new DateTime(2025, 3, 20, 12, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 2, SenderId = 5, ReceiverId = 10, Content = "Thank you, I will be there.", SentAt = new DateTime(2025, 3, 20, 13, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 3, SenderId = 10, ReceiverId = 6, Content = "Follow-up appointment scheduled.", SentAt = new DateTime(2025, 3, 20, 14, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 4, SenderId = 6, ReceiverId = 10, Content = "Got it, thanks!", SentAt = new DateTime(2025, 3, 20, 15, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 5, SenderId = 11, ReceiverId = 7, Content = "Please describe your headache symptoms.", SentAt = new DateTime(2025, 3, 20, 16, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 6, SenderId = 7, ReceiverId = 11, Content = "I have migraines often.", SentAt = new DateTime(2025, 3, 20, 17, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 7, SenderId = 11, ReceiverId = 8, Content = "Neurological exam scheduled.", SentAt = new DateTime(2025, 3, 20, 18, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 8, SenderId = 8, ReceiverId = 11, Content = "Thank you, doctor!", SentAt = new DateTime(2025, 3, 20, 19, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 9, SenderId = 12, ReceiverId = 9, Content = "Please bring your child’s vaccination record.", SentAt = new DateTime(2025, 3, 20, 20, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 10, SenderId = 9, ReceiverId = 12, Content = "Will do, thanks!", SentAt = new DateTime(2025, 3, 20, 21, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 11, SenderId = 12, ReceiverId = 4, Content = "Follow-up for your child scheduled.", SentAt = new DateTime(2025, 3, 20, 22, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 12, SenderId = 4, ReceiverId = 12, Content = "Thank you!", SentAt = new DateTime(2025, 3, 20, 23, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 13, SenderId = 13, ReceiverId = 5, Content = "Please describe your bone pain.", SentAt = new DateTime(2025, 3, 21, 8, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 14, SenderId = 5, ReceiverId = 13, Content = "It’s in my knee.", SentAt = new DateTime(2025, 3, 21, 9, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 15, SenderId = 13, ReceiverId = 6, Content = "Joint stiffness appointment scheduled.", SentAt = new DateTime(2025, 3, 21, 10, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 16, SenderId = 6, ReceiverId = 13, Content = "Thank you, I’ll be there.", SentAt = new DateTime(2025, 3, 21, 11, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 17, SenderId = 14, ReceiverId = 7, Content = "Please describe your skin rash.", SentAt = new DateTime(2025, 3, 21, 12, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 18, SenderId = 7, ReceiverId = 14, Content = "It’s red and itchy.", SentAt = new DateTime(2025, 3, 21, 13, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 19, SenderId = 14, ReceiverId = 8, Content = "Dermatology consult scheduled.", SentAt = new DateTime(2025, 3, 21, 14, 0, 0, DateTimeKind.Utc), IsRead = false },
                new Message { Id = 20, SenderId = 8, ReceiverId = 14, Content = "Thank you, I’ll bring my records.", SentAt = new DateTime(2025, 3, 21, 15, 0, 0, DateTimeKind.Utc), IsRead = false }
            );
        }
    }
}