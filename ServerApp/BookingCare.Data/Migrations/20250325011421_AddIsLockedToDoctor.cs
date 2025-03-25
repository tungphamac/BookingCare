using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsLockedToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalRecordId",
                table: "Patients",
                column: "MedicalRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MedicalRecords_MedicalRecordId",
                table: "Patients",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalRecords_MedicalRecordId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MedicalRecordId",
                table: "Patients");
        }
    }
}
