using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlkho.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialName_Image_ImageID",
                table: "MaterialName");

            migrationBuilder.DropIndex(
                name: "IX_MaterialName_ImageID",
                table: "MaterialName");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "MaterialName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "MaterialName",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialName_ImageID",
                table: "MaterialName",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialName_Image_ImageID",
                table: "MaterialName",
                column: "ImageID",
                principalTable: "Image",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
