using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JccApi.Migrations
{
    public partial class Change_GiftEntity_PK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_gift",
                table: "gift");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gift",
                table: "gift",
                columns: new[] { "child_id", "god_parent_id", "gif_type_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_gift",
                table: "gift");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gift",
                table: "gift",
                columns: new[] { "child_id", "god_parent_id" });
        }
    }
}
