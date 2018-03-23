using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class DistributedQuantityRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionOrderDistributedLength",
                table: "MaterialDistributionNoteDetails",
                newName: "DistributedLength");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistributedLength",
                table: "MaterialDistributionNoteDetails",
                newName: "ProductionOrderDistributedLength");
        }
    }
}
