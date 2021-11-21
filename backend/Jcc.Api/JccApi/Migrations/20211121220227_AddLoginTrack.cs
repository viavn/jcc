using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JccApi.Migrations
{
    public partial class AddLoginTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "god_parents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_login",
                table: "god_parents",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_date",
                table: "god_parents");

            migrationBuilder.DropColumn(
                name: "user_login",
                table: "god_parents");
        }
    }
}
