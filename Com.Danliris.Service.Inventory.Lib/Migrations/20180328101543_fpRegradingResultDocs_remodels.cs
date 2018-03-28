using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class fpRegradingResultDocs_remodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupplierCode",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierId",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                table: "fpRegradingResultDocs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierCode",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "SupplierName",
                table: "fpRegradingResultDocs");
        }
    }
}
