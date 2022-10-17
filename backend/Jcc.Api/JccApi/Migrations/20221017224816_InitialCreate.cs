using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    age = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    clothes_size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    shoes_size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    legal_responsible = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    family_acronym = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    family_phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    family_address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_children", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    password = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    user_type_id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "god_parents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_clothes_selected = table.Column<bool>(type: "boolean", nullable: false),
                    is_shoes_selected = table.Column<bool>(type: "boolean", nullable: false),
                    is_gift_selected = table.Column<bool>(type: "boolean", nullable: false),
                    ChildId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_god_parents_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_god_parents_ChildId",
                table: "god_parents",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_god_parents_UserId",
                table: "god_parents",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "god_parents");

            migrationBuilder.DropTable(
                name: "user_types");

            migrationBuilder.DropTable(
                name: "children");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
