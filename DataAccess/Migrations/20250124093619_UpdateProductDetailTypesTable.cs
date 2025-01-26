using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductDetailTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductDetailTypeId",
                table: "ProductDetails",
                column: "ProductDetailTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_ProductDetailTypes_ProductDetailTypeId",
                table: "ProductDetails",
                column: "ProductDetailTypeId",
                principalTable: "ProductDetailTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_ProductDetailTypes_ProductDetailTypeId",
                table: "ProductDetails");

            migrationBuilder.DropTable(
                name: "ProductDetailTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_ProductDetailTypeId",
                table: "ProductDetails");
        }
    }
}
