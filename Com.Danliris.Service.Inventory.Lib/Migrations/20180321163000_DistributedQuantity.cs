using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class DistributedQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceivedLength",
                table: "MaterialDistributionNoteDetails",
                newName: "DistributedLength");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "MaterialsRequestNotes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "DistributedLength",
                table: "MaterialsRequestNote_Items",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "ProductionOrderIsCompleted",
                table: "MaterialsRequestNote_Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "MaterialDistributionNoteDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProductionOrderIsCompleted",
                table: "MaterialDistributionNoteDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "MaterialsRequestNotes");

            migrationBuilder.DropColumn(
                name: "DistributedLength",
                table: "MaterialsRequestNote_Items");

            migrationBuilder.DropColumn(
                name: "ProductionOrderIsCompleted",
                table: "MaterialsRequestNote_Items");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "MaterialDistributionNoteDetails");

            migrationBuilder.DropColumn(
                name: "ProductionOrderIsCompleted",
                table: "MaterialDistributionNoteDetails");

            migrationBuilder.RenameColumn(
                name: "DistributedLength",
                table: "MaterialDistributionNoteDetails",
                newName: "ReceivedLength");
        }
    }
}
