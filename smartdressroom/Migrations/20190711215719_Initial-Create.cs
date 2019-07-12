using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smartdressroom.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<byte[]>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CollectionModels",
                columns: table => new
                {
                    ID = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionModels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ClothesModels",
                columns: table => new
                {
                    ID = table.Column<byte[]>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Size = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    ImgFormat = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true),
                    CollectionID = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClothesModels_CollectionModels_CollectionID",
                        column: x => x.CollectionID,
                        principalTable: "CollectionModels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothesModels_CollectionID",
                table: "ClothesModels",
                column: "CollectionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ClothesModels");

            migrationBuilder.DropTable(
                name: "CollectionModels");
        }
    }
}
