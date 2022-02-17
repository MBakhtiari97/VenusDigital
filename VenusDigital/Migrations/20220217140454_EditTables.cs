using Microsoft.EntityFrameworkCore.Migrations;

namespace VenusDigital.Migrations
{
    public partial class EditTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Users_UsersUserId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductsProductId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Products_ProductsProductId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Products_ProductsProductId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGalleries_Products_ProductsProductId",
                table: "ProductGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductsProductId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UsersUserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Products_ProductsProductId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_UsersUserId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_UsersUserId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ProductsProductId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductsProductId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UsersUserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductGalleries_ProductsProductId",
                table: "ProductGalleries");

            migrationBuilder.DropIndex(
                name: "IX_Items_ProductsProductId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Features_ProductsProductId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductsProductId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UsersUserId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "ProductGalleries");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "TotallPrice",
                table: "Cart",
                newName: "TotalPrice");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProductId",
                table: "Tags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalleries_ProductId",
                table: "ProductGalleries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProductId",
                table: "Items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProductId",
                table: "Features",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductId",
                table: "Categories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Users_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductId",
                table: "Categories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Products_ProductId",
                table: "Features",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Products_ProductId",
                table: "Items",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Products_ProductId",
                table: "Tags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Users_UserId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Products_ProductId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Products_ProductId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGalleries_Products_ProductId",
                table: "ProductGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Products_ProductId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Users_UserId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ProductId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductGalleries_ProductId",
                table: "ProductGalleries");

            migrationBuilder.DropIndex(
                name: "IX_Items_ProductId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Features_ProductId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Cart",
                newName: "TotallPrice");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "WishLists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "ProductGalleries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Features",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UsersUserId",
                table: "WishLists",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProductsProductId",
                table: "Tags",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductsProductId",
                table: "Reviews",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UsersUserId",
                table: "Reviews",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGalleries_ProductsProductId",
                table: "ProductGalleries",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProductsProductId",
                table: "Items",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProductsProductId",
                table: "Features",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductsProductId",
                table: "Categories",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UsersUserId",
                table: "Cart",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Users_UsersUserId",
                table: "Cart",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductsProductId",
                table: "Categories",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Products_ProductsProductId",
                table: "Features",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Products_ProductsProductId",
                table: "Items",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGalleries_Products_ProductsProductId",
                table: "ProductGalleries",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductsProductId",
                table: "Reviews",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UsersUserId",
                table: "Reviews",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Products_ProductsProductId",
                table: "Tags",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Users_UsersUserId",
                table: "WishLists",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
