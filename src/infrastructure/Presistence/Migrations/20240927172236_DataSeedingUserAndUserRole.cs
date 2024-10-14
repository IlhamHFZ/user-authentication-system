using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingUserAndUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0afb2aa1-d314-485c-8552-e29a546b1321"), 0, "4e5cec10-7588-4d47-af01-188af4897106", "Kuma Power", null, false, false, null, null, null, null, null, false, null, false, "Hikaru Utada" },
                    { new Guid("253cd4c1-4b3f-4a2a-83ae-269c3bdb7879"), 0, "31c83843-2fec-42e3-9182-018f22d0ab54", "Miku 39", null, false, false, null, null, null, null, null, false, null, false, "Hatsune Miku" },
                    { new Guid("656b6830-6908-4d5a-81a6-a20d98bf7d2d"), 0, "bef89bcb-2233-408c-b97a-79edcde02d85", "Luka", null, false, false, null, null, null, null, null, false, null, false, "Megurin Luka" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a2b46be9-b47b-45eb-9083-bc0d2387c462"), new Guid("0afb2aa1-d314-485c-8552-e29a546b1321") },
                    { new Guid("a2b46be9-b47b-45eb-9083-bc0d2387c462"), new Guid("253cd4c1-4b3f-4a2a-83ae-269c3bdb7879") },
                    { new Guid("a2b46be9-b47b-45eb-9083-bc0d2387c462"), new Guid("656b6830-6908-4d5a-81a6-a20d98bf7d2d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a2b46be9-b47b-45eb-9083-bc0d2387c462"), new Guid("0afb2aa1-d314-485c-8552-e29a546b1321") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a2b46be9-b47b-45eb-9083-bc0d2387c462"), new Guid("253cd4c1-4b3f-4a2a-83ae-269c3bdb7879") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a2b46be9-b47b-45eb-9083-bc0d2387c462"), new Guid("656b6830-6908-4d5a-81a6-a20d98bf7d2d") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0afb2aa1-d314-485c-8552-e29a546b1321"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("253cd4c1-4b3f-4a2a-83ae-269c3bdb7879"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("656b6830-6908-4d5a-81a6-a20d98bf7d2d"));
        }
    }
}
