using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer_Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    Customer_Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Rating_value = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Review_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Review", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Review_Customer_Id",
                table: "Customer_Review",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Review_Product_Id",
                table: "Customer_Review",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Review_Product_Id_Review_Date",
                table: "Customer_Review",
                columns: new[] { "Product_Id", "Review_Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer_Review");
        }
    }
}
