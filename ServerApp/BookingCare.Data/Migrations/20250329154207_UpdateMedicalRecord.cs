using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MedicalRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "MedicalRecords",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "MedicalRecords",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prescription",
                table: "MedicalRecords",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MedicalRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MedicalRecords",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy", "Diagnosis", "Notes", "Prescription", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Cảm cúm thông thường", "Nghỉ ngơi nhiều, uống đủ nước", "Paracetamol 500mg, uống 2 lần/ngày", null });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "DoctorId", "Status", "Time", "TimeSlot", "WorkDate" },
                values: new object[,]
                {
                    { 3, 3, 0, new DateTime(2025, 3, 20, 14, 0, 0, 0, DateTimeKind.Utc), "15:00-16:00", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, 0, new DateTime(2025, 3, 20, 14, 0, 0, 0, DateTimeKind.Utc), "15:00-16:00", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_CreatedBy",
                table: "MedicalRecords",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Doctors_CreatedBy",
                table: "MedicalRecords",
                column: "CreatedBy",
                principalTable: "Doctors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Doctors_CreatedBy",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_CreatedBy",
                table: "MedicalRecords");

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Prescription",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MedicalRecords");
        }
    }
}
