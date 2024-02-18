using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestrictDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmmenityRoomType_Ammenities_AmmenitiesAmmenityID",
                table: "AmmenityRoomType");

            migrationBuilder.DropForeignKey(
                name: "FK_AmmenityRoomType_RoomTypes_RoomTypesRoomTypeID",
                table: "AmmenityRoomType");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RoomTypes_RoomTypeID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomImages_RoomTypes_RoomTypeID",
                table: "RoomImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeID",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 1,
                column: "NewsDate",
                value: new DateTime(2023, 6, 15, 18, 45, 20, 314, DateTimeKind.Local).AddTicks(6466));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 2,
                column: "NewsDate",
                value: new DateTime(2023, 6, 15, 18, 45, 20, 314, DateTimeKind.Local).AddTicks(6494));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3,
                column: "NewsDate",
                value: new DateTime(2023, 6, 15, 18, 45, 20, 314, DateTimeKind.Local).AddTicks(6497));

            migrationBuilder.AddForeignKey(
                name: "FK_AmmenityRoomType_Ammenities_AmmenitiesAmmenityID",
                table: "AmmenityRoomType",
                column: "AmmenitiesAmmenityID",
                principalTable: "Ammenities",
                principalColumn: "AmmenityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AmmenityRoomType_RoomTypes_RoomTypesRoomTypeID",
                table: "AmmenityRoomType",
                column: "RoomTypesRoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerID",
                table: "Bookings",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_RoomTypes_RoomTypeID",
                table: "Bookings",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingID",
                table: "Payments",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImages_RoomTypes_RoomTypeID",
                table: "RoomImages",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmmenityRoomType_Ammenities_AmmenitiesAmmenityID",
                table: "AmmenityRoomType");

            migrationBuilder.DropForeignKey(
                name: "FK_AmmenityRoomType_RoomTypes_RoomTypesRoomTypeID",
                table: "AmmenityRoomType");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RoomTypes_RoomTypeID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomImages_RoomTypes_RoomTypeID",
                table: "RoomImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeID",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 1,
                column: "NewsDate",
                value: new DateTime(2023, 6, 10, 7, 22, 36, 157, DateTimeKind.Local).AddTicks(1643));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 2,
                column: "NewsDate",
                value: new DateTime(2023, 6, 10, 7, 22, 36, 157, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3,
                column: "NewsDate",
                value: new DateTime(2023, 6, 10, 7, 22, 36, 157, DateTimeKind.Local).AddTicks(1662));

            migrationBuilder.AddForeignKey(
                name: "FK_AmmenityRoomType_Ammenities_AmmenitiesAmmenityID",
                table: "AmmenityRoomType",
                column: "AmmenitiesAmmenityID",
                principalTable: "Ammenities",
                principalColumn: "AmmenityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmmenityRoomType_RoomTypes_RoomTypesRoomTypeID",
                table: "AmmenityRoomType",
                column: "RoomTypesRoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerID",
                table: "Bookings",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_RoomTypes_RoomTypeID",
                table: "Bookings",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingID",
                table: "Payments",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomImages_RoomTypes_RoomTypeID",
                table: "RoomImages",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
