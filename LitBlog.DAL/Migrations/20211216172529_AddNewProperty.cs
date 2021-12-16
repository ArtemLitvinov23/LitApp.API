using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitChat.DAL.Migrations
{
    public partial class AddNewProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdded",
                table: "FavoritesUsers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdded",
                table: "FavoritesUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
