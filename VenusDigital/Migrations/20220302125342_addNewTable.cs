using Microsoft.EntityFrameworkCore.Migrations;

namespace VenusDigital.Migrations
{
    public partial class addNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderProcessId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderProcess",
                columns: table => new
                {
                    OrderProcessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    IsReferred = table.Column<bool>(type: "bit", nullable: false),
                    ReferredDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProcess", x => x.OrderProcessId);
                    table.ForeignKey(
                        name: "FK_OrderProcess_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderProcessId",
                table: "Order",
                column: "OrderProcessId",
                unique: true,
                filter: "[OrderProcessId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcess_OrderId",
                table: "OrderProcess",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderProcess_OrderProcessId",
                table: "Order",
                column: "OrderProcessId",
                principalTable: "OrderProcess",
                principalColumn: "OrderProcessId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderProcess_OrderProcessId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderProcess");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderProcessId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderProcessId",
                table: "Order");
        }
    }
}
