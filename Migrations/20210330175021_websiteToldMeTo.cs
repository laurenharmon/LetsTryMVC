using Microsoft.EntityFrameworkCore.Migrations;

namespace LetsTryMVC.Migrations
{
    public partial class websiteToldMeTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Categories_ProductCategoryId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_ProductCategoryId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "Photo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "Photo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_ProductCategoryId",
                table: "Photo",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Categories_ProductCategoryId",
                table: "Photo",
                column: "ProductCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
