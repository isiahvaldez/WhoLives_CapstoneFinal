using Microsoft.EntityFrameworkCore.Migrations;

namespace WhoLives.DataAccess.Migrations
{
    public partial class changedisActivegetSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "InventoryItems");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "InventoryItems",
                nullable: false,
                defaultValue: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "InventoryItems");
        }
    }
}
