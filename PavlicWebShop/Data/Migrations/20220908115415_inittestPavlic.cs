using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PavlicWebShop.Data.Migrations
{
    public partial class inittestPavlic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductPhoto",
                table: "Product",
                newName: "ProductImgUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductImgUrl",
                table: "Product",
                newName: "ProductPhoto");
        }
    }
}
