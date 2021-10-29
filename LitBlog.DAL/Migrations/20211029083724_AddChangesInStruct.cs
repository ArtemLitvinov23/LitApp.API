using Microsoft.EntityFrameworkCore.Migrations;

namespace LitBlog.DAL.Migrations
{
    public partial class AddChangesInStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
