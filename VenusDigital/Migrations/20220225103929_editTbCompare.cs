using Microsoft.EntityFrameworkCore.Migrations;

namespace VenusDigital.Migrations
{
    public partial class editTbCompare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_Compare_ProductId",
                table: "Compare",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compare_Products_ProductId",
                table: "Compare",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compare_Products_ProductId",
                table: "Compare");

            migrationBuilder.DropIndex(
                name: "IX_Compare_ProductId",
                table: "Compare");

            migrationBuilder.CreateTable(
                name: "CompareProducts",
                columns: table => new
                {
                    CompareId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompareProducts", x => new { x.CompareId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_CompareProducts_Compare_CompareId",
                        column: x => x.CompareId,
                        principalTable: "Compare",
                        principalColumn: "CompareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompareProducts_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompareProducts_ProductsProductId",
                table: "CompareProducts",
                column: "ProductsProductId");
        }
    }
}
