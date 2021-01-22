using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceLayer.Migrations
{
    public partial class MeetingsUserIsVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUsers_Meetings_MeetingsId",
                table: "MeetingUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingUsers_MeetingsId",
                table: "MeetingUsers");

            migrationBuilder.DropColumn(
                name: "MeetingsId",
                table: "MeetingUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsVote",
                table: "MeetingUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUsers_MeetingId",
                table: "MeetingUsers",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUsers_Meetings_MeetingId",
                table: "MeetingUsers",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUsers_Meetings_MeetingId",
                table: "MeetingUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingUsers_MeetingId",
                table: "MeetingUsers");

            migrationBuilder.DropColumn(
                name: "IsVote",
                table: "MeetingUsers");

            migrationBuilder.AddColumn<int>(
                name: "MeetingsId",
                table: "MeetingUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUsers_MeetingsId",
                table: "MeetingUsers",
                column: "MeetingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUsers_Meetings_MeetingsId",
                table: "MeetingUsers",
                column: "MeetingsId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
