using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class StockRequestNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockTransferNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 255, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 255, nullable: true),
                    SourceStorageCode = table.Column<string>(maxLength: 255, nullable: true),
                    SourceStorageId = table.Column<string>(maxLength: 255, nullable: true),
                    SourceStorageName = table.Column<string>(maxLength: 255, nullable: true),
                    TargetStorageCode = table.Column<string>(maxLength: 255, nullable: true),
                    TargetStorageId = table.Column<string>(maxLength: 255, nullable: true),
                    TargetStorageName = table.Column<string>(maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_StockTransferNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTransferNoteItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductId = table.Column<string>(maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(maxLength: 255, nullable: true),
                    StockQuantity = table.Column<double>(nullable: false),
                    StockTransferNoteId = table.Column<int>(nullable: false),
                    StockTransferNote_ItemId = table.Column<int>(nullable: true),
                    TransferedQuantity = table.Column<double>(nullable: false),
                    UomId = table.Column<string>(maxLength: 255, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_StockTransferNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransferNoteItems_StockTransferNotes_StockTransferNoteId",
                        column: x => x.StockTransferNoteId,
                        principalTable: "StockTransferNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransferNoteItems_StockTransferNoteItems_StockTransferNote_ItemId",
                        column: x => x.StockTransferNote_ItemId,
                        principalTable: "StockTransferNoteItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferNoteItems_StockTransferNoteId",
                table: "StockTransferNoteItems",
                column: "StockTransferNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferNoteItems_StockTransferNote_ItemId",
                table: "StockTransferNoteItems",
                column: "StockTransferNote_ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransferNoteItems");

            migrationBuilder.DropTable(
                name: "StockTransferNotes");
        }
    }
}
