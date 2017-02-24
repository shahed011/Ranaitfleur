using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ranaitfleur.Migrations
{
    public partial class AddedOrderItemLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLine_Items_ItemId",
                table: "CartLine");

            migrationBuilder.DropForeignKey(
                name: "FK_CartLine_Orders_OrderId",
                table: "CartLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartLine",
                table: "CartLine");

            migrationBuilder.DropIndex(
                name: "IX_CartLine_OrderId",
                table: "CartLine");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CartLine");

            migrationBuilder.RenameTable(
                name: "CartLine",
                newName: "CartLines");

            migrationBuilder.RenameIndex(
                name: "IX_CartLine_ItemId",
                table: "CartLines",
                newName: "IX_CartLines_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines",
                column: "CartLineId");

            migrationBuilder.CreateTable(
                name: "OrderItemsLine",
                columns: table => new
                {
                    OrderItemsLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemsLine", x => x.OrderItemsLineId);
                    table.ForeignKey(
                        name: "FK_OrderItemsLine_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemsLine_OrderId",
                table: "OrderItemsLine",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Items_ItemId",
                table: "CartLines",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Items_ItemId",
                table: "CartLines");

            migrationBuilder.DropTable(
                name: "OrderItemsLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines");

            migrationBuilder.RenameTable(
                name: "CartLines",
                newName: "CartLine");

            migrationBuilder.RenameIndex(
                name: "IX_CartLines_ItemId",
                table: "CartLine",
                newName: "IX_CartLine_ItemId");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CartLine",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartLine",
                table: "CartLine",
                column: "CartLineId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_OrderId",
                table: "CartLine",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLine_Items_ItemId",
                table: "CartLine",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartLine_Orders_OrderId",
                table: "CartLine",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
