using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erfa.ProductionManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Product_ProductNumber_Is_Uniqe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Erfa_Pro_Catalog_ProductNumber",
                schema: "production",
                table: "Erfa_Pro_Catalog");

            migrationBuilder.CreateIndex(
                name: "IX_Erfa_Pro_Catalog_ProductNumber",
                schema: "production",
                table: "Erfa_Pro_Catalog",
                column: "ProductNumber", 
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Erfa_Pro_Catalog_ProductNumber",
                schema: "production",
                table: "Erfa_Pro_Catalog");

            migrationBuilder.CreateIndex(
                name: "IX_Erfa_Pro_Catalog_ProductNumber",
                schema: "production",
                table: "Erfa_Pro_Catalog",
                column: "ProductNumber");
        }
    }
}
