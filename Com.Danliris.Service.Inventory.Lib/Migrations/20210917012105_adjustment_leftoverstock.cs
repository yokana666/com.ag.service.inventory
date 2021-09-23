using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Inventory.Lib.Migrations
{
    public partial class adjustment_leftoverstock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GarmentLeftoverWarehouseStocks_ReferenceType_UnitId_PONo_RONo_ProductId_UomId_LeftoverComodityId",
                table: "GarmentLeftoverWarehouseStocks");

            migrationBuilder.AlterColumn<string>(
                name: "CustomsCategory",
                table: "GarmentLeftoverWarehouseStocks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentLeftoverWarehouseStocks_ReferenceType_UnitId_PONo_RONo_ProductId_UomId_LeftoverComodityId_CustomsCategory",
                table: "GarmentLeftoverWarehouseStocks",
                columns: new[] { "ReferenceType", "UnitId", "PONo", "RONo", "ProductId", "UomId", "LeftoverComodityId", "CustomsCategory" },
                unique: true,
                filter: "[_IsDeleted]=(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GarmentLeftoverWarehouseStocks_ReferenceType_UnitId_PONo_RONo_ProductId_UomId_LeftoverComodityId_CustomsCategory",
                table: "GarmentLeftoverWarehouseStocks");

            migrationBuilder.AlterColumn<string>(
                name: "CustomsCategory",
                table: "GarmentLeftoverWarehouseStocks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentLeftoverWarehouseStocks_ReferenceType_UnitId_PONo_RONo_ProductId_UomId_LeftoverComodityId",
                table: "GarmentLeftoverWarehouseStocks",
                columns: new[] { "ReferenceType", "UnitId", "PONo", "RONo", "ProductId", "UomId", "LeftoverComodityId" },
                unique: true,
                filter: "[_IsDeleted]=(0)");
        }
    }
}
