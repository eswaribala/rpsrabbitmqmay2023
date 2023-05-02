﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryAPI.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Catalog_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Catalog_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Catalog_Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Product_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Purchase_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Type = table.Column<int>(type: "int", nullable: true),
                    Unit_Price = table.Column<long>(type: "bigint", nullable: true),
                    Catalog_Id_FK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Product_Id);
                    table.ForeignKey(
                        name: "FK_Product_Catalog_Catalog_Id_FK",
                        column: x => x.Catalog_Id_FK,
                        principalTable: "Catalog",
                        principalColumn: "Catalog_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Catalog_Id_FK",
                table: "Product",
                column: "Catalog_Id_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Catalog");
        }
    }
}
