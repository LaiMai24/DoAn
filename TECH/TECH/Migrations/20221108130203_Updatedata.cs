using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECH.Migrations
{
    public partial class Updatedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar",
                table: "products");

            migrationBuilder.DropColumn(
                name: "color",
                table: "products");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "products");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "short_desc",
                table: "products",
                newName: "trademark");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "products",
                newName: "price_sell");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "images",
                newName: "name");

            migrationBuilder.AddColumn<decimal>(
                name: "price_import",
                table: "products",
                type: "decimal(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "price_reduced",
                table: "products",
                type: "decimal(18,0)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price_import",
                table: "products");

            migrationBuilder.DropColumn(
                name: "price_reduced",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "trademark",
                table: "products",
                newName: "short_desc");

            migrationBuilder.RenameColumn(
                name: "price_sell",
                table: "products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "images",
                newName: "image");

            migrationBuilder.AddColumn<string>(
                name: "avatar",
                table: "products",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "products",
                type: "varchar(200)",
                nullable: true);
        }
    }
}
