using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncludeIdentityAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2010d5cb-2d96-4897-a008-765216ef79c4", null, "Receptionist", "RECEPTIONIST" },
                    { "5311985b-1e7e-4597-844f-7473ca7a557d", null, "Admin", "ADMIN" },
                    { "8c0114d0-8289-4d3c-b314-d404ab2d7add", null, "Waiter", "WAITER" },
                    { "bf3e6e61-2fba-4579-a91e-8806129a55a2", null, "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 1,
                column: "NewsDate",
                value: new DateTime(2023, 6, 18, 22, 15, 20, 654, DateTimeKind.Local).AddTicks(4377));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 2,
                column: "NewsDate",
                value: new DateTime(2023, 6, 18, 22, 15, 20, 654, DateTimeKind.Local).AddTicks(4392));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3,
                column: "NewsDate",
                value: new DateTime(2023, 6, 18, 22, 15, 20, 654, DateTimeKind.Local).AddTicks(4395));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2010d5cb-2d96-4897-a008-765216ef79c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5311985b-1e7e-4597-844f-7473ca7a557d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c0114d0-8289-4d3c-b314-d404ab2d7add");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf3e6e61-2fba-4579-a91e-8806129a55a2");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 1,
                column: "NewsDate",
                value: new DateTime(2023, 6, 16, 17, 53, 20, 738, DateTimeKind.Local).AddTicks(8372));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 2,
                column: "NewsDate",
                value: new DateTime(2023, 6, 16, 17, 53, 20, 738, DateTimeKind.Local).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3,
                column: "NewsDate",
                value: new DateTime(2023, 6, 16, 17, 53, 20, 738, DateTimeKind.Local).AddTicks(8391));
        }
    }
}
