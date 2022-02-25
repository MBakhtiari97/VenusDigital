using Microsoft.EntityFrameworkCore.Migrations;

namespace VenusDigital.Migrations
{
    public partial class addingTwoColumnsToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AppliedCoupon",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPriceWithCoupon",
                table: "Order",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedCoupon",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TotalPriceWithCoupon",
                table: "Order");
        }
    }
}
