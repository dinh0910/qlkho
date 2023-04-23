using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlkho.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "MaterialTopic",
                columns: table => new
                {
                    MaterialTopicID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTopic", x => x.MaterialTopicID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    MaterialTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.MaterialTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialName",
                columns: table => new
                {
                    MaterialNameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialTypeID = table.Column<int>(type: "int", nullable: false),
                    MaterialTopicID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialName", x => x.MaterialNameID);
                    table.ForeignKey(
                        name: "FK_MaterialName_Image_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Image",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialName_MaterialType_MaterialTypeID",
                        column: x => x.MaterialTypeID,
                        principalTable: "MaterialType",
                        principalColumn: "MaterialTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialName_MaterialTopic_MaterialTopicID",
                        column: x => x.MaterialTopicID,
                        principalTable: "MaterialTopic",
                        principalColumn: "MaterialTopicID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNameID = table.Column<int>(type: "int", nullable: false),
                    Expiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialID);
                    table.ForeignKey(
                        name: "FK_Material_MaterialName_MaterialNameID",
                        column: x => x.MaterialNameID,
                        principalTable: "MaterialName",
                        principalColumn: "MaterialNameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialLog",
                columns: table => new
                {
                    MaterialLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    Stored = table.Column<bool>(type: "bit", nullable: true),
                    TakeAway = table.Column<bool>(type: "bit", nullable: true),
                    TookAway = table.Column<bool>(type: "bit", nullable: true),
                    Returned = table.Column<bool>(type: "bit", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialLog", x => x.MaterialLogID);
                    table.ForeignKey(
                        name: "FK_MaterialLog_Material_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Material",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialLog_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_MaterialNameID",
                table: "Material",
                column: "MaterialNameID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLog_MaterialID",
                table: "MaterialLog",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLog_UserID",
                table: "MaterialLog",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialName_ImageID",
                table: "MaterialName",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialName_MaterialTypeID",
                table: "MaterialName",
                column: "MaterialTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialName_MaterialTopicID",
                table: "MaterialName",
                column: "MaterialTopicID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialLog");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "MaterialTopic");

            migrationBuilder.DropTable(
                name: "MaterialName");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "MaterialType");
        }
    }
}
