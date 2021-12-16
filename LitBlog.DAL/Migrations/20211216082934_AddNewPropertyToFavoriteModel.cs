using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitChat.DAL.Migrations
{
    public partial class AddNewPropertyToFavoriteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesUsers_Accounts_AccountId",
                table: "FavoritesUsers");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "FavoritesUsers",
                newName: "OwnerAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesUsers_AccountId",
                table: "FavoritesUsers",
                newName: "IX_FavoritesUsers_OwnerAccountId");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteUserAccountId",
                table: "FavoritesUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesUsers_Accounts_OwnerAccountId",
                table: "FavoritesUsers",
                column: "OwnerAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesUsers_Accounts_OwnerAccountId",
                table: "FavoritesUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteUserAccountId",
                table: "FavoritesUsers");

            migrationBuilder.RenameColumn(
                name: "OwnerAccountId",
                table: "FavoritesUsers",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesUsers_OwnerAccountId",
                table: "FavoritesUsers",
                newName: "IX_FavoritesUsers_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesUsers_Accounts_AccountId",
                table: "FavoritesUsers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
