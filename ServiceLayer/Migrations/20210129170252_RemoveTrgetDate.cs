using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceLayer.Migrations
{
    public partial class RemoveTrgetDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingEnd",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MeetingStart",
                table: "Meetings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MeetingEnd",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetingStart",
                table: "Meetings",
                nullable: true);
        }
    }
}
