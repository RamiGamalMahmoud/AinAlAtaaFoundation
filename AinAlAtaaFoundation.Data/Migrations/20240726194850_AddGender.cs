using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGender : Migration
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
                name: "gender_id",
                table: "family_members",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "genders",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name_en = table.Column<string>(type: "TEXT", nullable: true),
                    name_ar = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genders", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "genders",
                columns: new[] { "id", "name_ar", "name_en" },
                values: new object[,]
                {
                    { 1, "ذكر", null },
                    { 2, "أنثى", null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_family_members_gender_id",
                table: "family_members",
                column: "gender_id");

            migrationBuilder.AddForeignKey(
                name: "fk_family_members_genders_gender_id",
                table: "family_members",
                column: "gender_id",
                principalTable: "genders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_family_members_genders_gender_id",
                table: "family_members");

            migrationBuilder.DropTable(
                name: "genders");

            migrationBuilder.DropIndex(
                name: "ix_family_members_gender_id",
                table: "family_members");

            migrationBuilder.DropColumn(
                name: "gender_id",
                table: "family_members");

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
