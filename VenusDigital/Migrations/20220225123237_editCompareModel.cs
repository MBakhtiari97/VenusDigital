using Microsoft.EntityFrameworkCore.Migrations;

namespace VenusDigital.Migrations
{
    public partial class editCompareModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Compare",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Compare_UserId",
                table: "Compare",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compare_Users_UserId",
                table: "Compare",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compare_Users_UserId",
                table: "Compare");

            migrationBuilder.DropIndex(
                name: "IX_Compare_UserId",
                table: "Compare");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Compare");
        }
    }
}
