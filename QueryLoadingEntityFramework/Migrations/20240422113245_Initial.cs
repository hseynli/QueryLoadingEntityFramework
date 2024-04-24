using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueryLoadingEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe" },
                    { 2, "jane.smith@example.com", "Jane", "Smith" },
                    { 3, "emily.johnson@example.com", "Emily", "Johnson" },
                    { 4, "william.brown@example.com", "William", "Brown" },
                    { 5, "sophia.williams@example.com", "Sophia", "Williams" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "OrderDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 4, 17, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1133) },
                    { 2, 2, new DateTime(2024, 4, 18, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1151) },
                    { 3, 3, new DateTime(2024, 4, 19, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1152) },
                    { 4, 4, new DateTime(2024, 4, 20, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1153) },
                    { 5, 5, new DateTime(2024, 4, 21, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1153) },
                    { 6, 1, new DateTime(2024, 4, 17, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1154) },
                    { 7, 2, new DateTime(2024, 4, 18, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1155) },
                    { 8, 2, new DateTime(2024, 4, 19, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1156) },
                    { 9, 4, new DateTime(2024, 4, 20, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1157) },
                    { 10, 3, new DateTime(2024, 4, 21, 15, 32, 44, 908, DateTimeKind.Local).AddTicks(1158) }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "OrderId", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, 1, 10.5m, "Product1" },
                    { 2, 1, 20.5m, "Product2" },
                    { 3, 2, 30.5m, "Product3" },
                    { 4, 2, 40.5m, "Product4" },
                    { 5, 3, 50.5m, "Product5" },
                    { 6, 4, 60.5m, "Product6" },
                    { 7, 5, 70.5m, "Product7" },
                    { 8, 2, 65.5m, "Product8" },
                    { 9, 2, 40.5m, "Product9" },
                    { 10, 3, 54.5m, "Product10" },
                    { 11, 4, 78.5m, "Product11" },
                    { 12, 5, 70.5m, "Product12" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
