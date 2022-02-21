using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitChat.DAL.Migrations
{
    public partial class AddedNewEntityFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Accounts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestById = table.Column<int>(type: "int", nullable: false),
                    RequestToId = table.Column<int>(type: "int", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestFlags = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friends_Accounts_RequestById",
                        column: x => x.RequestById,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Friends_Accounts_RequestToId",
                        column: x => x.RequestToId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RequestById",
                table: "Friends",
                column: "RequestById");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RequestToId",
                table: "Friends",
                column: "RequestToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteAccountId",
                table: "FavoritesUsers",
                type: "int",
                nullable: true);
        }
    }
}
