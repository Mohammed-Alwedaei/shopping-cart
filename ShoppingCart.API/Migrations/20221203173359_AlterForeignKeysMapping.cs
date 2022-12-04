using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.API.Migrations
{
    /// <inheritdoc />
    public partial class AlterForeignKeysMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingCartDetails_ShoppingCartDetailsModelId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCartHeaders_ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartDetails_ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShoppingCartDetailsModelId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails");

            migrationBuilder.DropColumn(
                name: "ShoppingCartDetailsModelId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShoppingCartDetailsModelId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartDetails_ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails",
                column: "ShoppingCartHeaderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingCartDetailsModelId",
                table: "Products",
                column: "ShoppingCartDetailsModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingCartDetails_ShoppingCartDetailsModelId",
                table: "Products",
                column: "ShoppingCartDetailsModelId",
                principalTable: "ShoppingCartDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCartHeaders_ShoppingCartHeaderModelId",
                table: "ShoppingCartDetails",
                column: "ShoppingCartHeaderModelId",
                principalTable: "ShoppingCartHeaders",
                principalColumn: "Id");
        }
    }
}
