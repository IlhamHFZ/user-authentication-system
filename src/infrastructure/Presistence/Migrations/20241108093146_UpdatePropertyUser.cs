using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizeDisplayName",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0afb2aa1-d314-485c-8552-e29a546b1321"),
                columns: new[] { "ConcurrencyStamp", "NormalizeDisplayName" },
                values: new object[] { "d10c147f-d7a3-40c0-8225-bf553504f4eb", "kuma power" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("253cd4c1-4b3f-4a2a-83ae-269c3bdb7879"),
                columns: new[] { "ConcurrencyStamp", "NormalizeDisplayName" },
                values: new object[] { "624a9ada-6f37-402c-8d73-3fd773c868f2", "miku 39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("656b6830-6908-4d5a-81a6-a20d98bf7d2d"),
                columns: new[] { "ConcurrencyStamp", "NormalizeDisplayName" },
                values: new object[] { "f02ef5ab-6265-4098-a52c-40c42f977ee5", "luka" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizeDisplayName",
                table: "AspNetUsers");

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
    }
}
