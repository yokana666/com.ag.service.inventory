using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class fpRegradingResultDocs_remodels_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LengthBeforeReGrade",
                table: "fpRegradingResultDocsDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MachineCode",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MachineId",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MachineName",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "fpRegradingResultDocs",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "fpRegradingResultDocs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthBeforeReGrade",
                table: "fpRegradingResultDocsDetails");

            migrationBuilder.DropColumn(
                name: "MachineCode",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "MachineName",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "Operator",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "fpRegradingResultDocs");
        }
    }
}
