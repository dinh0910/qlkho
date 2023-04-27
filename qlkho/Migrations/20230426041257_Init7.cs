using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlkho.Migrations
{
    public partial class Init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lend",
                columns: table => new
                {
                    LendID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lend", x => x.LendID);
                    table.ForeignKey(
                        name: "FK_Lend_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LendLog",
                columns: table => new
                {
                    LendLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LendID = table.Column<int>(type: "int", nullable: false),
                    MaterialNameID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LendLog", x => x.LendLogID);
                    table.ForeignKey(
                        name: "FK_LendLog_Lend_LendID",
                        column: x => x.LendID,
                        principalTable: "Lend",
                        principalColumn: "LendID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LendLog_MaterialName_MaterialNameID",
                        column: x => x.MaterialNameID,
                        principalTable: "MaterialName",
                        principalColumn: "MaterialNameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LendLog_Unit_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lend_UserID",
                table: "Lend",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LendLog_LendID",
                table: "LendLog",
                column: "LendID");

            migrationBuilder.CreateIndex(
                name: "IX_LendLog_MaterialNameID",
                table: "LendLog",
                column: "MaterialNameID");

            migrationBuilder.CreateIndex(
                name: "IX_LendLog_UnitID",
                table: "LendLog",
                column: "UnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LendLog");

            migrationBuilder.DropTable(
                name: "Lend");
        }
    }
}
