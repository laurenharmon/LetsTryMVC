using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LetsTryMVC.Migrations
{
    public partial class FriendsList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendsListId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FriendsLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserIdId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendsLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendsLists_AspNetUsers_UserIdId",
                        column: x => x.UserIdId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FriendsListId",
                table: "AspNetUsers",
                column: "FriendsListId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendsLists_UserIdId",
                table: "FriendsLists",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FriendsLists_FriendsListId",
                table: "AspNetUsers",
                column: "FriendsListId",
                principalTable: "FriendsLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FriendsLists_FriendsListId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FriendsLists");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FriendsListId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FriendsListId",
                table: "AspNetUsers");
        }
    }
}
