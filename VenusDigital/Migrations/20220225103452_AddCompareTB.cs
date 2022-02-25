using Microsoft.EntityFrameworkCore.Migrations;

namespace VenusDigital.Migrations
{
    public partial class AddCompareTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compare",
                columns: table => new
                {
                    CompareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compare", x => x.CompareId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompareProducts");

            migrationBuilder.DropTable(
                name: "Compare");
        }
    }
}
