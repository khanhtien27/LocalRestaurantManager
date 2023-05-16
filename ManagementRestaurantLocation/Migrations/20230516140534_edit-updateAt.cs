using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementRestaurantLocation.Migrations
{
    /// <inheritdoc />
    public partial class editupdateAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Update_At",
                table: "Restaurents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Update_At",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Update_At",
                table: "Categories",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Update_At",
                table: "Restaurents");

            migrationBuilder.DropColumn(
                name: "Update_At",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Update_At",
                table: "Categories");
        }
    }
}
