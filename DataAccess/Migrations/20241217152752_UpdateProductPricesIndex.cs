using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductPricesIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId_IsCurrent",
                table: "ProductPrices");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId_IsCurrent",
                table: "ProductPrices",
                columns: new[] { "ProductId", "IsCurrent" },
                unique: true,
                filter: "[IsCurrent] =1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId_IsCurrent",
                table: "ProductPrices");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId_IsCurrent",
                table: "ProductPrices",
                columns: new[] { "ProductId", "IsCurrent" },
                unique: true);
        }
    }
}
