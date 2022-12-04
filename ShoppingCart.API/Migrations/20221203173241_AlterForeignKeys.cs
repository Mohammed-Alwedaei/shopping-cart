using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.API.Migrations
{
    /// <inheritdoc />
    public partial class AlterForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCartHeaders_ShoppingCartDetailsId",
                table: "ShoppingCartDetails");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartDetailsId",
                table: "ShoppingCartDetails",
                newName: "ShoppingCartHeaderModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartDetails_ShoppingCartDetailsId",
                table: "ShoppingCartDetails",
                newName: "IX_ShoppingCartDetails_ShoppingCartHeaderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCartHeaders_ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails",
                column: "ShoppingCartHeaderModelId",
                principalTable: "ShoppingCartHeaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCartHeaders_ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails",
                newName: "ShoppingCartDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartDetails_ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails",
                newName: "IX_ShoppingCartDetails_ShoppingCartDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCartHeaders_ShoppingCartDetailsId",
                table: "ShoppingCartDetails",
                column: "ShoppingCartDetailsId",
                principalTable: "ShoppingCartHeaders",
                principalColumn: "Id");
        }
    }
}
