using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class AlterFPRegradingResultDocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeBefore",
                table: "fpRegradingResultDocsDetails");

            migrationBuilder.DropColumn(
                name: "LengthBeforeReGrade",
                table: "fpRegradingResultDocsDetails");

            migrationBuilder.AddColumn<string>(
                name: "OriginalGrade",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalLength",
                table: "fpRegradingResultDocs",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalGrade",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "TotalLength",
                table: "fpRegradingResultDocs");

            migrationBuilder.AddColumn<string>(
                name: "GradeBefore",
                table: "fpRegradingResultDocsDetails",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LengthBeforeReGrade",
                table: "fpRegradingResultDocsDetails",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
