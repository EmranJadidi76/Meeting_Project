using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceLayer.Migrations
{
    public partial class Initialx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUsers_AspNetUsers_UsersId",
                table: "MeetingUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingUsers_UsersId",
                table: "MeetingUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "MeetingUsers");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUsers_UserId",
                table: "MeetingUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUsers_AspNetUsers_UserId",
                table: "MeetingUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUsers_AspNetUsers_UserId",
                table: "MeetingUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingUsers_UserId",
                table: "MeetingUsers");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "MeetingUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUsers_UsersId",
                table: "MeetingUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUsers_AspNetUsers_UsersId",
                table: "MeetingUsers",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
