using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smartdressroom.Migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CollectionID",
                table: "ClothesModels",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ClothesModels_CollectionID",
                table: "ClothesModels",
                column: "CollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothesModels_CollectionModels_CollectionID",
                table: "ClothesModels",
                column: "CollectionID",
                principalTable: "CollectionModels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothesModels_CollectionModels_CollectionID",
                table: "ClothesModels");

            migrationBuilder.DropTable(
                name: "CollectionModels");

            migrationBuilder.DropIndex(
                name: "IX_ClothesModels_CollectionID",
                table: "ClothesModels");

            migrationBuilder.DropColumn(
                name: "CollectionID",
                table: "ClothesModels");
        }
    }
}
