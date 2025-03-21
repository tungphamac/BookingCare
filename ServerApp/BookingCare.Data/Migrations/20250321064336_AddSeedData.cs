using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Doctor", "DOCTOR" },
                    { 3, null, "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "123 Admin St", "admin1.jpg", "concurrency1", "admin1@example.com", true, true, false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", null, false, "stamp1", false, "admin1@example.com" },
                    { 2, 0, "123 Doctor St", "doctor1.jpg", "concurrency2", "doctor1@example.com", true, true, false, null, "DOCTOR1@EXAMPLE.COM", "DOCTOR1@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", null, false, "stamp2", false, "doctor1@example.com" },
                    { 3, 0, "456 Doctor St", "doctor2.jpg", "concurrency3", "doctor2@example.com", true, false, false, null, "DOCTOR2@EXAMPLE.COM", "DOCTOR2@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", null, false, "stamp3", false, "doctor2@example.com" },
                    { 4, 0, "456 Patient St", "patient1.jpg", "concurrency4", "patient1@example.com", true, false, false, null, "PATIENT1@EXAMPLE.COM", "PATIENT1@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", null, false, "stamp4", false, "patient1@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Address", "CreateAt", "Introduction", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "789 Clinic St", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), "Top clinic in the city", "City Clinic", 1234567890 },
                    { 2, "456 Health St", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), "Comprehensive care", "Health Center", 987654321 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Heart specialist", "cardio.jpg", "Cardiology" },
                    { 2, "Brain specialist", "neuro.jpg", "Neurology" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "UserId", "Achievement", "ClinicId", "Description", "SpecializationId" },
                values: new object[,]
                {
                    { 2, "Best Doctor 2023", 1, "Experienced cardiologist", 1 },
                    { 3, "Top Neurologist 2023", 2, "Expert in brain disorders", 2 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "UserId", "MedicalRecordId" },
                values: new object[] { 4, 1 });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "DoctorId", "Status", "TimeSlot", "WorkDate" },
                values: new object[,]
                {
                    { 1, 2, 0, "10:00-11:00", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, 0, "14:00-15:00", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "ClinicId", "Date", "DoctorId", "PatientId", "Reason", "ScheduleId", "Status", "Time" },
                values: new object[] { 1, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, "Checkup for heart condition", 1, "Scheduled", new TimeSpan(0, 10, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "AppointmentId", "Comment", "Rating" },
                values: new object[] { 1, 1, "Great service!", 5 });

            migrationBuilder.InsertData(
                table: "MedicalRecords",
                columns: new[] { "Id", "AppointmentId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MedicalRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
