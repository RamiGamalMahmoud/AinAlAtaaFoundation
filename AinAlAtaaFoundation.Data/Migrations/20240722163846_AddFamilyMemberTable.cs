using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyMemberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "applicant_id",
                table: "families",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "family_members",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    family_id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    birth_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    is_orphan = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_applicant = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_family_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_family_members_families_family_id",
                        column: x => x.family_id,
                        principalTable: "families",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_families_applicant_id",
                table: "families",
                column: "applicant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_family_members_family_id",
                table: "family_members",
                column: "family_id");

            migrationBuilder.AddForeignKey(
                name: "fk_families_family_members_applicant_id",
                table: "families",
                column: "applicant_id",
                principalTable: "family_members",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_families_family_members_applicant_id",
                table: "families");

            migrationBuilder.DropTable(
                name: "family_members");

            migrationBuilder.DropIndex(
                name: "ix_families_applicant_id",
                table: "families");

            migrationBuilder.DropColumn(
                name: "applicant_id",
                table: "families");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
