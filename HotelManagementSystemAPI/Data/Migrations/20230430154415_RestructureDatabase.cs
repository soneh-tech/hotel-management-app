using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystemAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestructureDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Designations_DesignationID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemCategories_ItemCategoryID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_CustomerID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_CustomerID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeID",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Employees_EmployeeID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Items_ItemID",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "AmmenityRoomType");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CustomerID",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_EmployeeID",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ItemID",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomTypeID",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CustomerID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CustomerID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemCategoryID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DesignationID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Payments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "AmmenityRoomType",
                columns: table => new
                {
                    AmmenitiesAmmenityID = table.Column<int>(type: "int", nullable: false),
                    RoomTypesRoomTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmmenityRoomType", x => new { x.AmmenitiesAmmenityID, x.RoomTypesRoomTypeID });
                    table.ForeignKey(
                        name: "FK_AmmenityRoomType_Ammenities_AmmenitiesAmmenityID",
                        column: x => x.AmmenitiesAmmenityID,
                        principalTable: "Ammenities",
                        principalColumn: "AmmenityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmmenityRoomType_RoomTypes_RoomTypesRoomTypeID",
                        column: x => x.RoomTypesRoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerID",
                table: "Sales",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmployeeID",
                table: "Sales",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ItemID",
                table: "Sales",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerID",
                table: "Reviews",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerID",
                table: "Payments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemCategoryID",
                table: "Items",
                column: "ItemCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationID",
                table: "Employees",
                column: "DesignationID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomID",
                table: "Bookings",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_AmmenityRoomType_RoomTypesRoomTypeID",
                table: "AmmenityRoomType",
                column: "RoomTypesRoomTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomID",
                table: "Bookings",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Designations_DesignationID",
                table: "Employees",
                column: "DesignationID",
                principalTable: "Designations",
                principalColumn: "DesignationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemCategories_ItemCategoryID",
                table: "Items",
                column: "ItemCategoryID",
                principalTable: "ItemCategories",
                principalColumn: "ItemCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_CustomerID",
                table: "Payments",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_CustomerID",
                table: "Reviews",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerID",
                table: "Sales",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Employees_EmployeeID",
                table: "Sales",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Items_ItemID",
                table: "Sales",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
