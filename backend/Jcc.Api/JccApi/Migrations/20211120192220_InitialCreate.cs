using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JccApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "children",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    age = table.Column<string>(nullable: false),
                    clothes_size = table.Column<string>(nullable: false),
                    shoes_size = table.Column<string>(nullable: false),
                    legal_responsible = table.Column<string>(nullable: false),
                    family_acronym = table.Column<string>(nullable: false),
                    family_phone = table.Column<string>(nullable: false),
                    family_address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_children", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "god_parents",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    phone = table.Column<string>(nullable: false),
                    is_clothes_selected = table.Column<bool>(nullable: false),
                    is_shoes_selected = table.Column<bool>(nullable: false),
                    is_gift_selected = table.Column<bool>(nullable: false),
                    ChildId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_god_parents", x => x.id);
                    table.ForeignKey(
                        name: "FK_god_parents_children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "children",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_god_parents_ChildId",
                table: "god_parents",
                column: "ChildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "god_parents");

            migrationBuilder.DropTable(
                name: "children");
        }
    }
}
