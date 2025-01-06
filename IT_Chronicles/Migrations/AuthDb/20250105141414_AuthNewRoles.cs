using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IT_Chronicles.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AuthNewRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08356c8b-fe2a-4d0d-b385-b76579f2ae20", "08356c8b-fe2a-4d0d-b385-b76579f2ae20", "Admin", "ADMIN" },
                    { "83a26f2d-4b69-45e1-9b9c-4fb57ae4f67a", "83a26f2d-4b69-45e1-9b9c-4fb57ae4f67a", "SuperAdmin", "SUPERADMIN" },
                    { "f376f12e-f050-4966-acfa-c0042727714e", "f376f12e-f050-4966-acfa-c0042727714e", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38272fd9-900a-4c5e-951c-1755cd7a7fca",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51ab49f7-2b86-42c1-a9b2-7b9373d9ca58", "AQAAAAIAAYagAAAAEP1T6iSCVxQ2cCQskp90fSr4KKr6miiiv22isvgwUzinOpmlEf4uQtQCjlAW05odyg==", "7d3632c1-0a70-4b50-b182-b2af2cb2abde" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "08356c8b-fe2a-4d0d-b385-b76579f2ae20", "38272fd9-900a-4c5e-951c-1755cd7a7fca" },
                    { "83a26f2d-4b69-45e1-9b9c-4fb57ae4f67a", "38272fd9-900a-4c5e-951c-1755cd7a7fca" },
                    { "f376f12e-f050-4966-acfa-c0042727714e", "38272fd9-900a-4c5e-951c-1755cd7a7fca" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "08356c8b-fe2a-4d0d-b385-b76579f2ae20", "38272fd9-900a-4c5e-951c-1755cd7a7fca" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "83a26f2d-4b69-45e1-9b9c-4fb57ae4f67a", "38272fd9-900a-4c5e-951c-1755cd7a7fca" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f376f12e-f050-4966-acfa-c0042727714e", "38272fd9-900a-4c5e-951c-1755cd7a7fca" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08356c8b-fe2a-4d0d-b385-b76579f2ae20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83a26f2d-4b69-45e1-9b9c-4fb57ae4f67a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f376f12e-f050-4966-acfa-c0042727714e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38272fd9-900a-4c5e-951c-1755cd7a7fca",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a91faaf-aa4e-430c-9990-3cd0e95caaf0", "AQAAAAIAAYagAAAAEIFYJHirNYvUNUDR+TO+LlQRz2p+FD+KrwODvfiF2FZJ7UoycMEc6XOvxb5FhYos6g==", "553b9d68-51cc-4aeb-8152-fff6ad06d74c" });
        }
    }
}
