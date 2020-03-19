using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WhoLives.DataAccess.Migrations
{
    public partial class InitialWithAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    InventoryItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedAsPartOfAssembly = table.Column<bool>(nullable: false),
                    IsAssembly = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TotalLooseQty = table.Column<int>(nullable: false),
                    ListRetailCost = table.Column<decimal>(type: "money", nullable: false),
                    ListWholesaleCost = table.Column<decimal>(type: "money", nullable: false),
                    ReorderQty = table.Column<int>(nullable: false),
                    MaxQty = table.Column<int>(nullable: false),
                    MeasureID = table.Column<int>(nullable: false),
                    MeasureAmnt = table.Column<decimal>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    TempRequired = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    AssemblyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.InventoryItemID);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Measures_MeasureID",
                        column: x => x.MeasureID,
                        principalTable: "Measures",
                        principalColumn: "MeasureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseOrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOrdered = table.Column<DateTime>(nullable: false),
                    StatusChangeDate = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    PO = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrderID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(nullable: true),
                    VendorWebsite = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorID);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyItems",
                columns: table => new
                {
                    AssemblyItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssemblyID = table.Column<int>(nullable: false),
                    InventoryItemID = table.Column<int>(nullable: false),
                    ItemQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyItems", x => x.AssemblyItemID);
                    table.ForeignKey(
                        name: "FK_AssemblyItems_InventoryItems_AssemblyID",
                        column: x => x.AssemblyID,
                        principalTable: "InventoryItems",
                        principalColumn: "InventoryItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyItems_InventoryItems_InventoryItemID",
                        column: x => x.InventoryItemID,
                        principalTable: "InventoryItems",
                        principalColumn: "InventoryItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryItemID = table.Column<int>(nullable: false),
                    PurchaseOrderID = table.Column<int>(nullable: false),
                    VendorID = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    QuantityOrdered = table.Column<int>(nullable: false),
                    QuantityReceived = table.Column<int>(nullable: false),
                    ItemReceived = table.Column<bool>(nullable: false),
                    DateDelivered = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItems_InventoryItems_InventoryItemID",
                        column: x => x.InventoryItemID,
                        principalTable: "InventoryItems",
                        principalColumn: "InventoryItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_PurchaseOrders_PurchaseOrderID",
                        column: x => x.PurchaseOrderID,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorItems",
                columns: table => new
                {
                    VendorItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryItemID = table.Column<int>(nullable: false),
                    VendorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorItems", x => x.VendorItemId);
                    table.ForeignKey(
                        name: "FK_VendorItems_InventoryItems_InventoryItemID",
                        column: x => x.InventoryItemID,
                        principalTable: "InventoryItems",
                        principalColumn: "InventoryItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorItems_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyItems_AssemblyID",
                table: "AssemblyItems",
                column: "AssemblyID");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyItems_InventoryItemID",
                table: "AssemblyItems",
                column: "InventoryItemID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_MeasureID",
                table: "InventoryItems",
                column: "MeasureID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_InventoryItemID",
                table: "OrderItems",
                column: "InventoryItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PurchaseOrderID",
                table: "OrderItems",
                column: "PurchaseOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_VendorID",
                table: "OrderItems",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorItems_InventoryItemID",
                table: "VendorItems",
                column: "InventoryItemID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorItems_VendorID",
                table: "VendorItems",
                column: "VendorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssemblyItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "VendorItems");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
