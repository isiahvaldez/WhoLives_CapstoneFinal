using Microsoft.EntityFrameworkCore.Migrations;

namespace WhoLives.DataAccess.Migrations
{
    public partial class vendorisActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Vendors",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Vendors");
        }
    }
}
