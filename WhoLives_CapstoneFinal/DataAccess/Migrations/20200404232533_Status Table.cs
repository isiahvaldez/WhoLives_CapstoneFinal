using Microsoft.EntityFrameworkCore.Migrations;

namespace WhoLives.DataAccess.Migrations
{
    public partial class StatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "PurchaseOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_StatusID",
                table: "PurchaseOrders",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Statuses_StatusID",
                table: "PurchaseOrders",
                column: "StatusID",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Statuses_StatusID",
                table: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_StatusID",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
