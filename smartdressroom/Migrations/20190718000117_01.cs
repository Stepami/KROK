using Microsoft.EntityFrameworkCore.Migrations;

namespace smartdressroom.Migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "ClothesModels",
                newName: "VendorCode");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "ClothesModels",
                newName: "ImagesCount");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CollectionModels",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImgFormat",
                table: "ClothesModels",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "ClothesModels",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sizes",
                table: "ClothesModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sizes",
                table: "ClothesModels");

            migrationBuilder.RenameColumn(
                name: "VendorCode",
                table: "ClothesModels",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "ImagesCount",
                table: "ClothesModels",
                newName: "Code");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CollectionModels",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ImgFormat",
                table: "ClothesModels",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "ClothesModels",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
