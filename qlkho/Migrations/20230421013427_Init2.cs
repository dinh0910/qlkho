using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlkho.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "MaterialName",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitID",
                table: "ImportLog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ImportLog_UnitID",
                table: "ImportLog",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportLog_Unit_UnitID",
                table: "ImportLog",
                column: "UnitID",
                principalTable: "Unit",
                principalColumn: "UnitID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportLog_Unit_UnitID",
                table: "ImportLog");

            migrationBuilder.DropIndex(
                name: "IX_ImportLog_UnitID",
                table: "ImportLog");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "MaterialName");

            migrationBuilder.DropColumn(
                name: "UnitID",
                table: "ImportLog");
        }
    }
}
