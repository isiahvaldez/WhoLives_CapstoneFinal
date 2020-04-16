using Microsoft.EntityFrameworkCore.Migrations;

namespace WhoLives.DataAccess.Migrations
{
    public partial class added_Wholesale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WholeSaleCost",
                table: "InventoryItems",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WholeSaleCost",
                table: "InventoryItems");
        }
    }
}
