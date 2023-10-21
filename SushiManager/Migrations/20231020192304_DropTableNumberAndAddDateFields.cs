using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SushiRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableNumber",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubmitDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "TableNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
