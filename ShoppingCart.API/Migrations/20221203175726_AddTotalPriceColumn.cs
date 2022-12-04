using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPriceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingCartDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingCartHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingCartHeaders");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingCartDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
