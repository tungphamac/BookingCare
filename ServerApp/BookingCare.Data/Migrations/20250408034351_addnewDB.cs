using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class addnewDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateExpire = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicSpecialization",
                columns: table => new
                {
                    ClinicsId = table.Column<int>(type: "int", nullable: false),
                    SpecializationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicSpecialization", x => new { x.ClinicsId, x.SpecializationsId });
                    table.ForeignKey(
                        name: "FK_ClinicSpecialization_Clinics_ClinicsId",
                        column: x => x.ClinicsId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicSpecialization_Specializations_SpecializationsId",
                        column: x => x.SpecializationsId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Achievement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Prescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Doctors_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { 1, 0, "123 Admin St", "admin1.jpg", "concurrency1", "admin1@example.com", true, true, true, null, "ADMIN1@EXAMPLE.COM", "ADMIN1@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567890", false, "stamp1", false, "admin1@example.com" },
                    { 2, 0, "123 Doctor St", "doctor1.jpg", "concurrency2", "doctor1@example.com", true, true, true, null, "DOCTOR1@EXAMPLE.COM", "DOCTOR1@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567891", false, "stamp2", false, "doctor1@example.com" },
                    { 3, 0, "456 Doctor St", "doctor2.jpg", "concurrency3", "doctor2@example.com", true, false, true, null, "DOCTOR2@EXAMPLE.COM", "DOCTOR2@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567892", false, "stamp3", false, "doctor2@example.com" },
                    { 4, 0, "456 Patient St", "patient1.jpg", "concurrency4", "patient1@example.com", true, false, true, null, "PATIENT1@EXAMPLE.COM", "PATIENT1@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567893", false, "stamp4", false, "patient1@example.com" },
                    { 5, 0, "101 Patient St", "patient2.jpg", "concurrency5", "patient2@example.com", true, false, true, null, "PATIENT2@EXAMPLE.COM", "PATIENT2@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567894", false, "stamp5", false, "patient2@example.com" },
                    { 6, 0, "202 Patient St", "patient3.jpg", "concurrency6", "patient3@example.com", true, true, true, null, "PATIENT3@EXAMPLE.COM", "PATIENT3@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567895", false, "stamp6", false, "patient3@example.com" },
                    { 7, 0, "303 Patient St", "patient4.jpg", "concurrency7", "patient4@example.com", true, false, true, null, "PATIENT4@EXAMPLE.COM", "PATIENT4@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567896", false, "stamp7", false, "patient4@example.com" },
                    { 8, 0, "404 Patient St", "patient5.jpg", "concurrency8", "patient5@example.com", true, true, true, null, "PATIENT5@EXAMPLE.COM", "PATIENT5@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567897", false, "stamp8", false, "patient5@example.com" },
                    { 9, 0, "505 Patient St", "patient6.jpg", "concurrency9", "patient6@example.com", true, false, true, null, "PATIENT6@EXAMPLE.COM", "PATIENT6@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567898", false, "stamp9", false, "patient6@example.com" },
                    { 10, 0, "606 Doctor St", "doctor3.jpg", "concurrency10", "doctor3@example.com", true, true, true, null, "DOCTOR3@EXAMPLE.COM", "DOCTOR3@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567899", false, "stamp10", false, "doctor3@example.com" },
                    { 11, 0, "707 Doctor St", "doctor4.jpg", "concurrency11", "doctor4@example.com", true, false, true, null, "DOCTOR4@EXAMPLE.COM", "DOCTOR4@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567800", false, "stamp11", false, "doctor4@example.com" },
                    { 12, 0, "808 Doctor St", "doctor5.jpg", "concurrency12", "doctor5@example.com", true, true, true, null, "DOCTOR5@EXAMPLE.COM", "DOCTOR5@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567801", false, "stamp12", false, "doctor5@example.com" },
                    { 13, 0, "909 Doctor St", "doctor6.jpg", "concurrency13", "doctor6@example.com", true, false, true, null, "DOCTOR6@EXAMPLE.COM", "DOCTOR6@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567802", false, "stamp13", false, "doctor6@example.com" },
                    { 14, 0, "1010 Doctor St", "doctor7.jpg", "concurrency14", "doctor7@example.com", true, true, true, null, "DOCTOR7@EXAMPLE.COM", "DOCTOR7@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...hashedpassword...", "1234567803", false, "stamp14", false, "doctor7@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Address", "CreateAt", "Introduction", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "789 Clinic St", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Top clinic in the city", "City Clinic", 1234567890 },
                    { 2, "456 Health St", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Comprehensive care", "Health Center", 1234567894 },
                    { 3, "123 Sunshine Ave", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Family healthcare provider", "Sunshine Clinic", 1551112233 },
                    { 4, "789 Green Valley Rd", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Specialized medical services", "Green Valley Hospital", 1442223344 },
                    { 5, "456 Blue Sky Ln", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Modern healthcare solutions", "Blue Sky Clinic", 1334445566 },
                    { 6, "111 Hope St", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Quality care for all", "Hope Clinic", 1234567801 },
                    { 7, "222 Life Rd", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Advanced medical services", "Life Hospital", 1234567802 },
                    { 8, "333 Peace Ave", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Holistic healthcare", "Peace Center", 1234567803 },
                    { 9, "444 Care Ln", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Patient-centered care", "Care Clinic", 1234567804 },
                    { 10, "555 Wellness Dr", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Promoting wellness", "Wellness Hub", 1234567805 },
                    { 11, "666 Harmony St", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Balanced healthcare", "Harmony Clinic", 1234567806 },
                    { 12, "777 Vitality Rd", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Vital health solutions", "Vitality Center", 1234567807 },
                    { 13, "888 Unity Ave", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Unified care approach", "Unity Hospital", 1234567808 },
                    { 14, "999 Bright Ln", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Brightening lives", "Bright Clinic", 1234567809 },
                    { 15, "101 Healing Dr", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Healing for all", "Healing Point", 1234567810 },
                    { 16, "222 Evergreen St", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Everlasting care", "Evergreen Clinic", 1234567811 },
                    { 17, "333 Star Rd", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Shining health services", "Star Hospital", 1234567812 },
                    { 18, "444 Golden Ave", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Golden standard care", "Golden Care", 1234567813 },
                    { 19, "555 Silver Ln", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Silver quality care", "Silver Clinic", 1234567814 },
                    { 20, "666 Platinum Dr", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Platinum healthcare", "Platinum Center", 1234567815 },
                    { 21, "777 Diamond St", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Diamond care services", "Diamond Clinic", 1234567816 },
                    { 22, "888 Ruby Rd", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Ruby health solutions", "Ruby Hospital", 1234567817 },
                    { 23, "999 Sapphire Ave", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Sapphire care provider", "Sapphire Clinic", 1234567818 },
                    { 24, "1010 Emerald Ln", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Emerald healthcare", "Emerald Center", 1234567819 },
                    { 25, "1111 Opal Dr", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Opal care solutions", "Opal Clinic", 1234567820 }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Heart specialist", "cardio.jpg", "Cardiology" },
                    { 2, "Brain specialist", "neuro.jpg", "Neurology" },
                    { 3, "Child health specialist", "pediatrics.jpg", "Pediatrics" },
                    { 4, "Bone and joint specialist", "ortho.jpg", "Orthopedics" },
                    { 5, "Skin specialist", "derm.jpg", "Dermatology" },
                    { 6, "Cancer specialist", "onco.jpg", "Oncology" },
                    { 7, "Digestive system specialist", "gastro.jpg", "Gastroenterology" },
                    { 8, "Hormone specialist", "endo.jpg", "Endocrinology" },
                    { 9, "Mental health specialist", "psych.jpg", "Psychiatry" },
                    { 10, "Eye specialist", "ophthal.jpg", "Ophthalmology" },
                    { 11, "Urinary system specialist", "uro.jpg", "Urology" },
                    { 12, "Women’s health specialist", "gyn.jpg", "Gynecology" },
                    { 13, "Joint and autoimmune specialist", "rheum.jpg", "Rheumatology" },
                    { 14, "Lung specialist", "pulmo.jpg", "Pulmonology" },
                    { 15, "Kidney specialist", "nephro.jpg", "Nephrology" },
                    { 16, "Blood specialist", "hema.jpg", "Hematology" },
                    { 17, "Allergy specialist", "allergy.jpg", "Allergy" },
                    { 18, "Infectious disease specialist", "infect.jpg", "Infectious Disease" },
                    { 19, "Cosmetic surgery specialist", "plastic.jpg", "Plastic Surgery" },
                    { 20, "Anesthesia specialist", "anes.jpg", "Anesthesiology" },
                    { 21, "Imaging specialist", "radio.jpg", "Radiology" },
                    { 22, "Disease diagnosis specialist", "patho.jpg", "Pathology" },
                    { 23, "Elderly care specialist", "geri.jpg", "Geriatrics" },
                    { 24, "Sports injury specialist", "sports.jpg", "Sports Medicine" },
                    { 25, "Emergency care specialist", "emerg.jpg", "Emergency Medicine" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 2, 10 },
                    { 2, 11 },
                    { 2, 12 },
                    { 2, 13 },
                    { 2, 14 }
                });

            migrationBuilder.InsertData(
                table: "ClinicSpecialization",
                columns: new[] { "ClinicsId", "SpecializationsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 3 },
                    { 3, 5 },
                    { 4, 1 },
                    { 4, 4 },
                    { 4, 5 },
                    { 5, 2 },
                    { 5, 3 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 },
                    { 10, 10 },
                    { 11, 11 },
                    { 12, 12 },
                    { 13, 13 },
                    { 14, 14 },
                    { 15, 15 },
                    { 16, 16 },
                    { 17, 17 },
                    { 18, 18 },
                    { 19, 19 },
                    { 20, 20 },
                    { 21, 21 },
                    { 22, 22 },
                    { 23, 23 },
                    { 24, 24 },
                    { 25, 25 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "UserId", "Achievement", "ClinicId", "Description", "SpecializationId" },
                values: new object[,]
                {
                    { 2, "Best Doctor 2023", 1, "Experienced cardiologist", 1 },
                    { 3, "Top Neurologist 2023", 2, "Expert in brain disorders", 2 },
                    { 10, "Oncology Expert 2024", 1, "Cancer specialist", 1 },
                    { 11, "Neuro Award 2024", 2, "Brain expert", 2 },
                    { 12, "Pediatric Star 2024", 3, "Child health expert", 3 },
                    { 13, "Ortho Innovator 2024", 4, "Bone specialist", 4 },
                    { 14, "Derm Leader 2024", 5, "Skin care expert", 5 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "IsRead", "ReceiverId", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { 1, "Please prepare for your cancer screening.", false, 5, 10, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Thank you, I will be there.", false, 10, 5, new DateTime(2025, 3, 20, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Follow-up appointment scheduled.", false, 6, 10, new DateTime(2025, 3, 20, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Got it, thanks!", false, 10, 6, new DateTime(2025, 3, 20, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Please describe your headache symptoms.", false, 7, 11, new DateTime(2025, 3, 20, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "I have migraines often.", false, 11, 7, new DateTime(2025, 3, 20, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "Neurological exam scheduled.", false, 8, 11, new DateTime(2025, 3, 20, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, "Thank you, doctor!", false, 11, 8, new DateTime(2025, 3, 20, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, "Please bring your child’s vaccination record.", false, 9, 12, new DateTime(2025, 3, 20, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, "Will do, thanks!", false, 12, 9, new DateTime(2025, 3, 20, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, "Follow-up for your child scheduled.", false, 4, 12, new DateTime(2025, 3, 20, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, "Thank you!", false, 12, 4, new DateTime(2025, 3, 20, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, "Please describe your bone pain.", false, 5, 13, new DateTime(2025, 3, 21, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, "It’s in my knee.", false, 13, 5, new DateTime(2025, 3, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, "Joint stiffness appointment scheduled.", false, 6, 13, new DateTime(2025, 3, 21, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, "Thank you, I’ll be there.", false, 13, 6, new DateTime(2025, 3, 21, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, "Please describe your skin rash.", false, 7, 14, new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, "It’s red and itchy.", false, 14, 7, new DateTime(2025, 3, 21, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, "Dermatology consult scheduled.", false, 8, 14, new DateTime(2025, 3, 21, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, "Thank you, I’ll bring my records.", false, 14, 8, new DateTime(2025, 3, 21, 15, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "UserId", "MedicalRecordId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 5, 2 },
                    { 6, 3 },
                    { 7, 4 },
                    { 8, 5 },
                    { 9, 6 }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "DoctorId", "Status", "Time", "TimeSlot", "WorkDate" },
                values: new object[,]
                {
                    { 1, 2, 0, new DateTime(2025, 3, 20, 10, 0, 0, 0, DateTimeKind.Utc), "10:00-11:00", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 3, 0, new DateTime(2025, 3, 20, 14, 0, 0, 0, DateTimeKind.Utc), "14:00-15:00", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, 0, new DateTime(2025, 3, 20, 14, 0, 0, 0, DateTimeKind.Utc), "15:00-16:00", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, 0, new DateTime(2025, 3, 20, 14, 0, 0, 0, DateTimeKind.Utc), "15:00-16:00", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 10, 0, new DateTime(2025, 3, 21, 8, 0, 0, 0, DateTimeKind.Utc), "08:00-09:00", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 10, 0, new DateTime(2025, 3, 21, 9, 0, 0, 0, DateTimeKind.Utc), "09:00-10:00", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 11, 0, new DateTime(2025, 3, 21, 10, 0, 0, 0, DateTimeKind.Utc), "10:00-11:00", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 11, 0, new DateTime(2025, 3, 21, 11, 0, 0, 0, DateTimeKind.Utc), "11:00-12:00", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 12, 0, new DateTime(2025, 3, 21, 13, 0, 0, 0, DateTimeKind.Utc), "13:00-14:00", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 12, 0, new DateTime(2025, 3, 21, 14, 0, 0, 0, DateTimeKind.Utc), "14:00-15:00", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 13, 0, new DateTime(2025, 3, 21, 15, 0, 0, 0, DateTimeKind.Utc), "15:00-16:00", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 13, 0, new DateTime(2025, 3, 21, 16, 0, 0, 0, DateTimeKind.Utc), "16:00-17:00", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 14, 0, new DateTime(2025, 3, 22, 8, 0, 0, 0, DateTimeKind.Utc), "08:00-09:00", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 14, 0, new DateTime(2025, 3, 22, 9, 0, 0, 0, DateTimeKind.Utc), "09:00-10:00", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 2, 0, new DateTime(2025, 3, 22, 10, 0, 0, 0, DateTimeKind.Utc), "10:00-11:00", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 2, 0, new DateTime(2025, 3, 22, 11, 0, 0, 0, DateTimeKind.Utc), "11:00-12:00", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 3, 0, new DateTime(2025, 3, 22, 13, 0, 0, 0, DateTimeKind.Utc), "13:00-14:00", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 3, 0, new DateTime(2025, 3, 22, 14, 0, 0, 0, DateTimeKind.Utc), "14:00-15:00", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 10, 0, new DateTime(2025, 3, 22, 15, 0, 0, 0, DateTimeKind.Utc), "15:00-16:00", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 10, 0, new DateTime(2025, 3, 22, 16, 0, 0, 0, DateTimeKind.Utc), "16:00-17:00", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 11, 0, new DateTime(2025, 3, 23, 8, 0, 0, 0, DateTimeKind.Utc), "08:00-09:00", new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 11, 0, new DateTime(2025, 3, 23, 9, 0, 0, 0, DateTimeKind.Utc), "09:00-10:00", new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 12, 0, new DateTime(2025, 3, 23, 10, 0, 0, 0, DateTimeKind.Utc), "10:00-11:00", new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 12, 0, new DateTime(2025, 3, 23, 11, 0, 0, 0, DateTimeKind.Utc), "11:00-12:00", new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "ClinicId", "CreatedAt", "Date", "DoctorId", "PatientId", "Reason", "ScheduleId", "Status", "Time" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 2, 4, "Checkup for heart condition", 1, 1, new TimeSpan(0, 10, 0, 0, 0) },
                    { 2, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 10, 5, "Cancer screening", 5, 1, new TimeSpan(0, 8, 0, 0, 0) },
                    { 3, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 10, 6, "Follow-up cancer", 6, 1, new TimeSpan(0, 9, 0, 0, 0) },
                    { 4, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 11, 7, "Headache check", 7, 1, new TimeSpan(0, 10, 0, 0, 0) },
                    { 5, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 11, 8, "Neurological exam", 8, 1, new TimeSpan(0, 11, 0, 0, 0) },
                    { 6, 3, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 12, 9, "Child checkup", 9, 1, new TimeSpan(0, 13, 0, 0, 0) },
                    { 7, 3, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 12, 4, "Pediatric follow-up", 10, 1, new TimeSpan(0, 14, 0, 0, 0) },
                    { 8, 4, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 13, 5, "Bone pain", 11, 1, new TimeSpan(0, 15, 0, 0, 0) },
                    { 9, 4, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 13, 6, "Joint stiffness", 12, 1, new TimeSpan(0, 16, 0, 0, 0) },
                    { 10, 5, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 14, 7, "Skin rash", 13, 1, new TimeSpan(0, 8, 0, 0, 0) },
                    { 11, 5, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 14, 8, "Dermatology consult", 14, 1, new TimeSpan(0, 9, 0, 0, 0) },
                    { 12, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 2, 9, "Heart checkup", 15, 1, new TimeSpan(0, 10, 0, 0, 0) },
                    { 13, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 2, 4, "Chest pain", 16, 1, new TimeSpan(0, 11, 0, 0, 0) },
                    { 14, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 3, 5, "Brain scan follow-up", 17, 1, new TimeSpan(0, 13, 0, 0, 0) },
                    { 15, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 3, 6, "Neurology consult", 18, 1, new TimeSpan(0, 14, 0, 0, 0) },
                    { 16, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 10, 7, "Oncology follow-up", 19, 1, new TimeSpan(0, 15, 0, 0, 0) },
                    { 17, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 10, 8, "Cancer treatment plan", 20, 1, new TimeSpan(0, 16, 0, 0, 0) },
                    { 18, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc), 11, 9, "Neurology exam", 21, 1, new TimeSpan(0, 8, 0, 0, 0) },
                    { 19, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc), 11, 4, "Head injury check", 22, 1, new TimeSpan(0, 9, 0, 0, 0) },
                    { 20, 3, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc), 12, 5, "Child vaccination", 23, 1, new TimeSpan(0, 10, 0, 0, 0) },
                    { 21, 3, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc), 12, 6, "Pediatric checkup", 24, 1, new TimeSpan(0, 11, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "AppointmentId", "Comment", "CreatedAt", "Rating" },
                values: new object[,]
                {
                    { 1, 1, "Great service!", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 2, 2, "Good care", new DateTime(2025, 3, 21, 9, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 3, 3, "Excellent service", new DateTime(2025, 3, 21, 10, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 4, 4, "Average experience", new DateTime(2025, 3, 21, 11, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 5, 5, "Very helpful", new DateTime(2025, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 6, 6, "Friendly staff", new DateTime(2025, 3, 21, 14, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 7, 7, "Great doctor", new DateTime(2025, 3, 21, 15, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 8, 8, "Good visit", new DateTime(2025, 3, 21, 16, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 9, 9, "Could improve", new DateTime(2025, 3, 21, 17, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 10, 10, "Highly recommend", new DateTime(2025, 3, 22, 9, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 11, 11, "Professional care", new DateTime(2025, 3, 22, 10, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 12, 12, "Very satisfied", new DateTime(2025, 3, 22, 11, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 13, 13, "Good experience", new DateTime(2025, 3, 22, 12, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 14, 14, "Okay service", new DateTime(2025, 3, 22, 14, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 15, 15, "Amazing doctor", new DateTime(2025, 3, 22, 15, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 16, 16, "Helpful session", new DateTime(2025, 3, 22, 16, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 17, 17, "Excellent care", new DateTime(2025, 3, 22, 17, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 18, 18, "Good consultation", new DateTime(2025, 3, 23, 9, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 19, 19, "Average visit", new DateTime(2025, 3, 23, 10, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 20, 20, "Very happy", new DateTime(2025, 3, 23, 11, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 21, 21, "Great pediatric care", new DateTime(2025, 3, 23, 12, 0, 0, 0, DateTimeKind.Utc), 4 }
                });

            migrationBuilder.InsertData(
                table: "MedicalRecords",
                columns: new[] { "Id", "AppointmentId", "CreatedAt", "CreatedBy", "Diagnosis", "Notes", "Prescription", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Cảm cúm thông thường", "Nghỉ ngơi nhiều, uống đủ nước", "Paracetamol 500mg, uống 2 lần/ngày", null },
                    { 2, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Hypertension", "Monitor blood pressure", "Lisinopril 10mg", null },
                    { 3, 3, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Diabetes", "Check blood sugar", "Metformin 500mg", null },
                    { 4, 4, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Allergy", "Avoid allergens", "Antihistamine", null },
                    { 5, 5, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Flu", "Stay hydrated", "Rest and fluids", null },
                    { 6, 6, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Migraine", "Rest in dark room", "Ibuprofen 200mg", null }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "AppointmentId", "CreatedAt", "IsRead", "Message", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 20/03/2025.", 4 },
                    { 2, 1, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient1@example.com đã đặt lịch hẹn vào ngày 20/03/2025.", 2 },
                    { 3, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 21/03/2025.", 5 },
                    { 4, 2, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient2@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", 10 },
                    { 5, 3, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 21/03/2025.", 6 },
                    { 6, 3, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient3@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", 10 },
                    { 7, 4, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 21/03/2025.", 7 },
                    { 8, 4, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient4@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", 11 },
                    { 9, 5, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 21/03/2025.", 8 },
                    { 10, 5, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient5@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", 11 },
                    { 11, 6, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 21/03/2025.", 9 },
                    { 12, 6, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient6@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", 12 },
                    { 13, 7, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 21/03/2025.", 4 },
                    { 14, 7, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient1@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", 12 },
                    { 15, 8, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 21/03/2025.", 5 },
                    { 16, 8, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient2@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", 13 },
                    { 17, 9, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 21/03/2025.", 6 },
                    { 18, 9, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient3@example.com đã đặt lịch hẹn vào ngày 21/03/2025.", 13 },
                    { 19, 10, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 22/03/2025.", 7 },
                    { 20, 10, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient4@example.com đã đặt lịch hẹn vào ngày 22/03/2025.", 14 },
                    { 21, 11, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bạn có lịch hẹn mới vào ngày 22/03/2025.", 8 },
                    { 22, 11, new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "Bệnh nhân patient5@example.com đã đặt lịch hẹn vào ngày 22/03/2025.", 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClinicId",
                table: "Appointments",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduleId",
                table: "Appointments",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicSpecialization_SpecializationsId",
                table: "ClinicSpecialization",
                column: "SpecializationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ClinicId",
                table: "Doctors",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_AppointmentId",
                table: "Feedbacks",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_AppointmentId",
                table: "MedicalRecords",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_CreatedBy",
                table: "MedicalRecords",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AppointmentId",
                table: "Notifications",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClinicSpecialization");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
