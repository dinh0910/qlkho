using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlkho.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit", table: "ImportLog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
