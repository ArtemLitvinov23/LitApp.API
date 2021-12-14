using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitChat.DAL.Migrations
{
    public partial class InitDatabase3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_FromUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_ToUserId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_FromUserId",
                table: "Messages",
                column: "FromUserId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_ToUserId",
                table: "Messages",
                column: "ToUserId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_FromUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Accounts_ToUserId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_FromUserId",
                table: "Messages",
                column: "FromUserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Accounts_ToUserId",
                table: "Messages",
                column: "ToUserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
