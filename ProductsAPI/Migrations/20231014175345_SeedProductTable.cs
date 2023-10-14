using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAPI.Migrations
{
    public partial class SeedProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreateDate", "Description", "ImageUrl", "Name", "Price", "Quantity", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2023, 10, 14, 23, 23, 44, 875, DateTimeKind.Local).AddTicks(6358), "Product 1 Description", "", "Product 1", 100, 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreateDate", "Description", "ImageUrl", "Name", "Price", "Quantity", "UpdatedDate" },
                values: new object[] { 2, new DateTime(2023, 10, 14, 23, 23, 44, 875, DateTimeKind.Local).AddTicks(6374), "Product 2 Description", "", "Product 2", 200, 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreateDate", "Description", "ImageUrl", "Name", "Price", "Quantity", "UpdatedDate" },
                values: new object[] { 3, new DateTime(2023, 10, 14, 23, 23, 44, 875, DateTimeKind.Local).AddTicks(6376), "Product 3 Description", "", "Product 3", 300, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
