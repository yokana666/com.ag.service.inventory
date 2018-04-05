using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class RemarkItemRequestNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FpReturProInvDocsDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FpReturProInvDocs",
                table: "FpReturProInvDocs");

            migrationBuilder.RenameTable(
                name: "FpReturProInvDocs",
                newName: "fpRegradingResultDocs");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "MaterialsRequestNote_Items",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "fpRegradingResultDocs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                table: "fpRegradingResultDocs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NoBonId",
                table: "fpRegradingResultDocs",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturnedToPurchasing",
                table: "fpRegradingResultDocs",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddColumn<string>(
                name: "SupplierCode",
                table: "fpRegradingResultDocs",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_fpRegradingResultDocs",
                table: "fpRegradingResultDocs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "fpRegradingResultDocsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    FpReturProInvDocsId = table.Column<int>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    GradeBefore = table.Column<string>(nullable: true),
                    Length = table.Column<double>(nullable: false),
                    LengthBeforeReGrade = table.Column<double>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Retur = table.Column<string>(nullable: true),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fpRegradingResultDocsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fpRegradingResultDocsDetails_fpRegradingResultDocs_FpReturProInvDocsId",
                        column: x => x.FpReturProInvDocsId,
                        principalTable: "fpRegradingResultDocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FPReturnInvToPurchasings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    No = table.Column<string>(maxLength: 255, nullable: true),
                    SupplierCode = table.Column<string>(maxLength: 255, nullable: true),
                    SupplierId = table.Column<string>(maxLength: 255, nullable: true),
                    SupplierName = table.Column<string>(maxLength: 255, nullable: true),
                    UnitName = table.Column<string>(maxLength: 255, nullable: true),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FPReturnInvToPurchasings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FPReturnInvToPurchasingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    FPRegradingResultDocsCode = table.Column<string>(maxLength: 255, nullable: true),
                    FPRegradingResultDocsId = table.Column<int>(nullable: false),
                    FPReturnInvToPurchasingId = table.Column<int>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductId = table.Column<string>(maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(maxLength: 255, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FPReturnInvToPurchasingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FPReturnInvToPurchasingDetails_FPReturnInvToPurchasings_FPReturnInvToPurchasingId",
                        column: x => x.FPReturnInvToPurchasingId,
                        principalTable: "FPReturnInvToPurchasings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fpRegradingResultDocsDetails_FpReturProInvDocsId",
                table: "fpRegradingResultDocsDetails",
                column: "FpReturProInvDocsId");

            migrationBuilder.CreateIndex(
                name: "IX_FPReturnInvToPurchasingDetails_FPReturnInvToPurchasingId",
                table: "FPReturnInvToPurchasingDetails",
                column: "FPReturnInvToPurchasingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fpRegradingResultDocsDetails");

            migrationBuilder.DropTable(
                name: "FPReturnInvToPurchasingDetails");

            migrationBuilder.DropTable(
                name: "FPReturnInvToPurchasings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_fpRegradingResultDocs",
                table: "fpRegradingResultDocs");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "MaterialsRequestNote_Items");

            migrationBuilder.DropColumn(
                name: "IsReturnedToPurchasing",
                table: "fpRegradingResultDocs");

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

            migrationBuilder.DropColumn(
                name: "SupplierCode",
                table: "fpRegradingResultDocs");

            migrationBuilder.RenameTable(
                name: "fpRegradingResultDocs",
                newName: "FpReturProInvDocs");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "FpReturProInvDocs",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                table: "FpReturProInvDocs",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoBonId",
                table: "FpReturProInvDocs",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FpReturProInvDocs",
                table: "FpReturProInvDocs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FpReturProInvDocsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    FpReturProInvDocsId = table.Column<int>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    SupplierId = table.Column<string>(nullable: true),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FpReturProInvDocsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FpReturProInvDocsDetails_FpReturProInvDocs_FpReturProInvDocsId",
                        column: x => x.FpReturProInvDocsId,
                        principalTable: "FpReturProInvDocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FpReturProInvDocsDetails_FpReturProInvDocsId",
                table: "FpReturProInvDocsDetails",
                column: "FpReturProInvDocsId");
        }
    }
}
