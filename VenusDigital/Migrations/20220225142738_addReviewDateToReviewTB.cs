using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VenusDigital.Migrations
{
    public partial class addReviewDateToReviewTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewCreateDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewCreateDate",
                table: "Reviews");
        }
    }
}
