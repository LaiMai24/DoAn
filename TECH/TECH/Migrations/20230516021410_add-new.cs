using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECH.Migrations
{
    public partial class addnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ishidden",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sizeId",
                table: "order_details",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "carts",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sizeId",
                table: "carts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ishidden",
                table: "products");

            migrationBuilder.DropColumn(
                name: "sizeId",
                table: "order_details");

            migrationBuilder.DropColumn(
                name: "sizeId",
                table: "carts");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);
        }
    }
}
