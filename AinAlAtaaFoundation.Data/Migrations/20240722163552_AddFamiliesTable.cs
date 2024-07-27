using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFamiliesTable : Migration
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
                name: "family_id",
                table: "phone",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "district_representatives",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 6, 2, 487, DateTimeKind.Local).AddTicks(8372));

            migrationBuilder.CreateTable(
                name: "family_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_family_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orphan_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orphan_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "social_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "families",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    branch_id = table.Column<int>(type: "INTEGER", nullable: false),
                    branch_representative_id = table.Column<int>(type: "INTEGER", nullable: false),
                    district_representative_id = table.Column<int>(type: "INTEGER", nullable: false),
                    address_id = table.Column<int>(type: "INTEGER", nullable: false),
                    social_status_id = table.Column<int>(type: "INTEGER", nullable: false),
                    family_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    orphan_type_id = table.Column<int>(type: "INTEGER", nullable: false),
                    ration_card = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_families", x => x.id);
                    table.ForeignKey(
                        name: "fk_families_address_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_families_branch_representatives_branch_representative_id",
                        column: x => x.branch_representative_id,
                        principalTable: "branch_representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_families_branches_branch_id",
                        column: x => x.branch_id,
                        principalTable: "branches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_families_district_representatives_district_representative_id",
                        column: x => x.district_representative_id,
                        principalTable: "district_representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_families_family_types_family_type_id",
                        column: x => x.family_type_id,
                        principalTable: "family_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_families_orphan_types_orphan_type_id",
                        column: x => x.orphan_type_id,
                        principalTable: "orphan_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_families_social_statuses_social_status_id",
                        column: x => x.social_status_id,
                        principalTable: "social_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "family_types",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "عام" },
                    { 2, "خاص" }
                });

            migrationBuilder.InsertData(
                table: "orphan_types",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "يتيم الأب" },
                    { 2, "يتيم الأم" },
                    { 3, "يتيم الأب و الأم" }
                });

            migrationBuilder.InsertData(
                table: "social_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "أيتام" },
                    { 2, "متعفف" },
                    { 3, "أرامل" },
                    { 4, "مطلقات" },
                    { 5, "الزوج مفقود" },
                    { 6, "أخرى" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_phone_family_id",
                table: "phone",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "ix_families_address_id",
                table: "families",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_families_branch_id",
                table: "families",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "ix_families_branch_representative_id",
                table: "families",
                column: "branch_representative_id");

            migrationBuilder.CreateIndex(
                name: "ix_families_district_representative_id",
                table: "families",
                column: "district_representative_id");

            migrationBuilder.CreateIndex(
                name: "ix_families_family_type_id",
                table: "families",
                column: "family_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_families_orphan_type_id",
                table: "families",
                column: "orphan_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_families_ration_card",
                table: "families",
                column: "ration_card",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_families_social_status_id",
                table: "families",
                column: "social_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_family_types_name",
                table: "family_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_orphan_types_name",
                table: "orphan_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_social_statuses_name",
                table: "social_statuses",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_phone_families_family_id",
                table: "phone",
                column: "family_id",
                principalTable: "families",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_phone_families_family_id",
                table: "phone");

            migrationBuilder.DropTable(
                name: "families");

            migrationBuilder.DropTable(
                name: "family_types");

            migrationBuilder.DropTable(
                name: "orphan_types");

            migrationBuilder.DropTable(
                name: "social_statuses");

            migrationBuilder.DropIndex(
                name: "ix_phone_family_id",
                table: "phone");

            migrationBuilder.DropColumn(
                name: "family_id",
                table: "phone");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "district_representatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 22, 13, 6, 2, 487, DateTimeKind.Local).AddTicks(8372),
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }
    }
}
