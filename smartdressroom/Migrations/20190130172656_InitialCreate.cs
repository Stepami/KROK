using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smartdressroom.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClothesModels",
                columns: table => new
                {
                    ID = table.Column<byte[]>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Size = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesModels", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothesModels");
        }
    }
}
