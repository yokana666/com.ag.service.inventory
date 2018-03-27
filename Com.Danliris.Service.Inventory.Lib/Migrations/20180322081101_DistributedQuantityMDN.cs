using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class DistributedQuantityMDN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistributedLength",
                table: "MaterialDistributionNoteDetails",
                newName: "ReceivedLength");

            migrationBuilder.AddColumn<DateTime>(
                name: "MaterialRequestNoteCreatedDateUtc",
                table: "MaterialDistributionNoteItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "ProductionOrderDistributedLength",
                table: "MaterialDistributionNoteDetails",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialRequestNoteCreatedDateUtc",
                table: "MaterialDistributionNoteItems");

            migrationBuilder.DropColumn(
                name: "ProductionOrderDistributedLength",
                table: "MaterialDistributionNoteDetails");

            migrationBuilder.RenameColumn(
                name: "ReceivedLength",
                table: "MaterialDistributionNoteDetails",
                newName: "DistributedLength");
        }
    }
}
