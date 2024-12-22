using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovePriceFromProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId_IsCurrent",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId_IsCurrent",
                table: "ProductPrices",
                columns: new[] { "ProductId", "IsCurrent" },
                unique: true,
                filter: "[IsCurrent] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId_IsCurrent",
                table: "ProductPrices");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId_IsCurrent",
                table: "ProductPrices",
                columns: new[] { "ProductId", "IsCurrent" },
                unique: true,
                filter: "[IsCurrent] =1");
        }
    }
}
