using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialsRequestNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Remark = table.Column<string>(maxLength: 100, nullable: true),
                    RequestType = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UnitCode = table.Column<string>(maxLength: 100, nullable: true),
                    UnitId = table.Column<string>(maxLength: 100, nullable: true),
                    UnitName = table.Column<string>(maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_MaterialsRequestNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialsRequestNote_Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Grade = table.Column<string>(maxLength: 100, nullable: true),
                    Length = table.Column<double>(nullable: false),
                    MaterialsRequestNoteId = table.Column<int>(nullable: false),
                    OrderQuantity = table.Column<double>(nullable: false),
                    OrderTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    OrderTypeId = table.Column<string>(maxLength: 100, nullable: true),
                    OrderTypeName = table.Column<string>(maxLength: 100, nullable: true),
                    ProductCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductId = table.Column<string>(maxLength: 100, nullable: true),
                    ProductName = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderId = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderNo = table.Column<string>(maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_MaterialsRequestNote_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialsRequestNote_Items_MaterialsRequestNotes_MaterialsRequestNoteId",
                        column: x => x.MaterialsRequestNoteId,
                        principalTable: "MaterialsRequestNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsRequestNote_Items_MaterialsRequestNoteId",
                table: "MaterialsRequestNote_Items",
                column: "MaterialsRequestNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialsRequestNote_Items");

            migrationBuilder.DropTable(
                name: "MaterialsRequestNotes");
        }
    }
}
