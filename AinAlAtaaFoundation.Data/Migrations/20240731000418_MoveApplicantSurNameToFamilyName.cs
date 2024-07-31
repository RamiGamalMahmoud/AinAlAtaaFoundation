using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoveApplicantSurNameToFamilyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_families_ration_card_owner_name",
                table: "families");

            migrationBuilder.DropColumn(
                name: "surname",
                table: "family_members");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "families",
                type: "VARCHAR(20)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "families");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "surname",
                table: "family_members",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_families_ration_card_owner_name",
                table: "families",
                column: "ration_card_owner_name",
                unique: true);
        }
    }
}
