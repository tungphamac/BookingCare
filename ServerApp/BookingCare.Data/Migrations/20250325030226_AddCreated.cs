using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "AppointmentId", "Comment", "CreatedAt", "Rating" },
                values: new object[] { 1, 1, "Great service!", new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), 5 });
        }
    }
}
