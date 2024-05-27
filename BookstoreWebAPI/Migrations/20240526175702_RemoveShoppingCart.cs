using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Clients_ClientId",
                table: "ShoppingCarts");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Clients_ClientId",
                table: "ShoppingCarts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Clients_ClientId",
                table: "ShoppingCarts");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Clients_ClientId",
                table: "ShoppingCarts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
