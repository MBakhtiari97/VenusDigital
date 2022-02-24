using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VenusDigital.Migrations
{
    public partial class addTwoItemToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTraceCode",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PaymentTraceCode",
                table: "Order");
        }
    }
}
