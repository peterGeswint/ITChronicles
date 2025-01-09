using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT_Chronicles.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class superadminpassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38272fd9-900a-4c5e-951c-1755cd7a7fca",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8f0457c-8d2a-4eb5-b21c-ceb2d430d257", "AQAAAAIAAYagAAAAEB0DxaQpopN59VOKidN/7AHOAWJjnoyuxqv8l2X7YNKkPIH4O4Y74Ffg+U7SEIL30g==", "ddc53591-dc11-481a-ae8c-6dc4c33ef0a1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38272fd9-900a-4c5e-951c-1755cd7a7fca",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51ab49f7-2b86-42c1-a9b2-7b9373d9ca58", "AQAAAAIAAYagAAAAEP1T6iSCVxQ2cCQskp90fSr4KKr6miiiv22isvgwUzinOpmlEf4uQtQCjlAW05odyg==", "7d3632c1-0a70-4b50-b182-b2af2cb2abde" });
        }
    }
}
