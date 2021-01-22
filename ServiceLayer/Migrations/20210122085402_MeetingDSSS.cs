using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceLayer.Migrations
{
    public partial class MeetingDSSS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeetingDate",
                table: "Meetings",
                newName: "MeetingStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetingEnd",
                table: "Meetings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingEnd",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "MeetingStart",
                table: "Meetings",
                newName: "MeetingDate");
        }
    }
}
