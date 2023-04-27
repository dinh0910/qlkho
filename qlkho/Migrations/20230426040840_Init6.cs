using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlkho.Migrations
{
    public partial class Init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Export");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Import",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Import");

            migrationBuilder.AddColumn<byte>(
                name: "Reason",
                table: "Export",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
