using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JccApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "family",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    contact_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_family", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gift_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gift_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "god_parent",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    contact_number = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_god_parent", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_person_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_legal_person_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "child",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    age = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    clothe_size = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    shoe_size = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    genre_type_id = table.Column<int>(type: "integer", nullable: false),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_child", x => x.id);
                    table.ForeignKey(
                        name: "FK_child_family_family_id",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_child_genre_genre_type_id",
                        column: x => x.genre_type_id,
                        principalTable: "genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "family_member",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    legal_person_type_id = table.Column<int>(type: "integer", nullable: false),
                    family_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_family_member", x => x.id);
                    table.ForeignKey(
                        name: "FK_family_member_family_family_id",
                        column: x => x.family_id,
                        principalTable: "family",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_family_member_legal_person_type_legal_person_type_id",
                        column: x => x.legal_person_type_id,
                        principalTable: "legal_person_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    user_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_user_type_user_type_id",
                        column: x => x.user_type_id,
                        principalTable: "user_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gift",
                columns: table => new
                {
                    child_id = table.Column<Guid>(type: "uuid", nullable: false),
                    god_parent_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gif_type_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_delivered = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gift", x => new { x.child_id, x.god_parent_id });
                    table.ForeignKey(
                        name: "FK_gift_child_child_id",
                        column: x => x.child_id,
                        principalTable: "child",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gift_gift_type_gif_type_id",
                        column: x => x.gif_type_id,
                        principalTable: "gift_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gift_god_parent_god_parent_id",
                        column: x => x.god_parent_id,
                        principalTable: "god_parent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gift_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_child_family_id",
                table: "child",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_child_genre_type_id",
                table: "child",
                column: "genre_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_family_member_family_id",
                table: "family_member",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_family_member_legal_person_type_id",
                table: "family_member",
                column: "legal_person_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_gift_gif_type_id",
                table: "gift",
                column: "gif_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_gift_god_parent_id",
                table: "gift",
                column: "god_parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_gift_user_id",
                table: "gift",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_user_type_id",
                table: "user",
                column: "user_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "family_member");

            migrationBuilder.DropTable(
                name: "gift");

            migrationBuilder.DropTable(
                name: "legal_person_type");

            migrationBuilder.DropTable(
                name: "child");

            migrationBuilder.DropTable(
                name: "gift_type");

            migrationBuilder.DropTable(
                name: "god_parent");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "family");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "user_type");
        }
    }
}
