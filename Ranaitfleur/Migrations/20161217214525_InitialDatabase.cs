using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ranaitfleur.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description1 = table.Column<string>(nullable: true),
                    Description2 = table.Column<string>(nullable: true),
                    Dimentions = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ItemType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NoOfItemInStock = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
