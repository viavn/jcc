using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JccApi.Migrations
{
    public partial class TrackingUserInGodParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_login",
                table: "god_parents");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "god_parents",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_god_parents_UserId",
                table: "god_parents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_god_parents_users_UserId",
                table: "god_parents",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_god_parents_users_UserId",
                table: "god_parents");

            migrationBuilder.DropIndex(
                name: "IX_god_parents_UserId",
                table: "god_parents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "god_parents");

            migrationBuilder.AddColumn<string>(
                name: "user_login",
                table: "god_parents",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
