using Microsoft.EntityFrameworkCore.Migrations;

namespace LitBlog.DAL.Migrations
{
    public partial class InitFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendsList_Accounts_AccountId",
                table: "FriendsList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendsList",
                table: "FriendsList");

            migrationBuilder.RenameTable(
                name: "FriendsList",
                newName: "Friends");

            migrationBuilder.RenameIndex(
                name: "IX_FriendsList_AccountId",
                table: "Friends",
                newName: "IX_Friends_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friends",
                table: "Friends",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Accounts_AccountId",
                table: "Friends",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Accounts_AccountId",
                table: "Friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friends",
                table: "Friends");

            migrationBuilder.RenameTable(
                name: "Friends",
                newName: "FriendsList");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_AccountId",
                table: "FriendsList",
                newName: "IX_FriendsList_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendsList",
                table: "FriendsList",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendsList_Accounts_AccountId",
                table: "FriendsList",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
