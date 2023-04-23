using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlkho.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.CreateTable(
                name: "Export",
                columns: table => new
                {
                    ExportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Export", x => x.ExportID);
                    table.ForeignKey(
                        name: "FK_Export_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportLog",
                columns: table => new
                {
                    ExportLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportID = table.Column<int>(type: "int", nullable: false),
                    MaterialNameID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportLog", x => x.ExportLogID);
                    table.ForeignKey(
                        name: "FK_ExportLog_Export_ExportID",
                        column: x => x.ExportID,
                        principalTable: "Export",
                        principalColumn: "ExportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportLog_MaterialName_MaterialNameID",
                        column: x => x.MaterialNameID,
                        principalTable: "MaterialName",
                        principalColumn: "MaterialNameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportLog_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Export_UserID",
                table: "Export",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportLog_ExportID",
                table: "ExportLog",
                column: "ExportID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportLog_MaterialNameID",
                table: "ExportLog",
                column: "MaterialNameID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportLog_UnitID",
                table: "ExportLog",
                column: "UnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportLog");

            migrationBuilder.DropTable(
                name: "Export");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                });
        }
    }
}
