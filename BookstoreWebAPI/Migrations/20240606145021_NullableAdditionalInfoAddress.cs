using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class NullableAdditionalInfoAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressClient_Addresses_AdressesId",
                table: "AddressClient");

            migrationBuilder.RenameColumn(
                name: "AdressesId",
                table: "AddressClient",
                newName: "AddressesId");

            migrationBuilder.AlterColumn<string>(
                name: "AddInfo",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressClient_Addresses_AddressesId",
                table: "AddressClient",
                column: "AddressesId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressClient_Addresses_AddressesId",
                table: "AddressClient");

            migrationBuilder.RenameColumn(
                name: "AddressesId",
                table: "AddressClient",
                newName: "AdressesId");

            migrationBuilder.AlterColumn<string>(
                name: "AddInfo",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressClient_Addresses_AdressesId",
                table: "AddressClient",
                column: "AdressesId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
