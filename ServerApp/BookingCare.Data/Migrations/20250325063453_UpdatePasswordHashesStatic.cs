using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordHashesStatic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGXs6jlQ0fcRlYEIS79CtCpXBe5FWbJ1DrY8WfEl1rfeWk1uYzS31Z26uSCKTdTLzg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBdqsT6m3NC+7WLrfiDuhbO8Q1erkBM5/mIXYTD6dXiY8IgOtLVSkgIwLQkKB/si3A==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBdqsT6m3NC+7WLrfiDuhbO8Q1erkBM5/mIXYTD6dXiY8IgOtLVSkgIwLQkKB/si3A==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELwmoQmUgF0dngxTkAHHQx1B/cyP4U3Af+3eekWM6ZLLVSHH9oFjnvZB653+9EsaMg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAE...hashedpassword...");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAE...hashedpassword...");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAE...hashedpassword...");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAE...hashedpassword...");
        }
    }
}
