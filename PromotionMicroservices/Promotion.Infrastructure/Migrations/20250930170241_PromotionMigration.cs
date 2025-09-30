using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promotion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PromotionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotion_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Promotion_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Category_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Category_Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotion_Details_Promotion_Promotion_Id",
                        column: x => x.Promotion_Id,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_End_Date",
                table: "Promotion",
                column: "End_Date");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_Start_Date",
                table: "Promotion",
                column: "Start_Date");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_Details_Promotion_Id_Product_Category_Id",
                table: "Promotion_Details",
                columns: new[] { "Promotion_Id", "Product_Category_Id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotion_Details");

            migrationBuilder.DropTable(
                name: "Promotion");
        }
    }
}
