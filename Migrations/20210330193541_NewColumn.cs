using Microsoft.EntityFrameworkCore.Migrations;

namespace LetsTryMVC.Migrations
{
    public partial class NewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Photo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_CategoryId",
                table: "Photo",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Categories_CategoryId",
                table: "Photo",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Categories_CategoryId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_CategoryId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Photo");
        }
    }
}
