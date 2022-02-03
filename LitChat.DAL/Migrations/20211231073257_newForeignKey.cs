using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitChat.DAL.Migrations
{
    public partial class newForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesUsers_Accounts_OwnerAccountId",
                table: "FavoritesUsers");

            migrationBuilder.DropIndex(
                name: "IX_FavoritesUsers_OwnerAccountId",
                table: "FavoritesUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FavoritesUsers_OwnerAccountId",
                table: "FavoritesUsers",
                column: "OwnerAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesUsers_Accounts_OwnerAccountId",
                table: "FavoritesUsers",
                column: "OwnerAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
