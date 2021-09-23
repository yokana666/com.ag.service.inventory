﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class adjustment_receipt_accessories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomsCategory",
                table: "GarmentLeftoverWarehouseReceiptAccessoryItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomsCategory",
                table: "GarmentLeftoverWarehouseReceiptAccessoryItems");
        }
    }
}
