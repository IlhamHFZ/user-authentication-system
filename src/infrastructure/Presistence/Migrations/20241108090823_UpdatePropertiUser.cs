using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertiUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0afb2aa1-d314-485c-8552-e29a546b1321"),
                column: "ConcurrencyStamp",
                value: "e07704bd-8529-4c44-b18b-55d788060f6e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("253cd4c1-4b3f-4a2a-83ae-269c3bdb7879"),
                column: "ConcurrencyStamp",
                value: "b085e760-aca4-4794-9e88-7f05198de1d5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("656b6830-6908-4d5a-81a6-a20d98bf7d2d"),
                column: "ConcurrencyStamp",
                value: "b01ac45d-772f-4557-9349-ae317317dbf2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0afb2aa1-d314-485c-8552-e29a546b1321"),
                column: "ConcurrencyStamp",
                value: "4e5cec10-7588-4d47-af01-188af4897106");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("253cd4c1-4b3f-4a2a-83ae-269c3bdb7879"),
                column: "ConcurrencyStamp",
                value: "31c83843-2fec-42e3-9182-018f22d0ab54");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("656b6830-6908-4d5a-81a6-a20d98bf7d2d"),
                column: "ConcurrencyStamp",
                value: "bef89bcb-2233-408c-b97a-79edcde02d85");
        }
    }
}
