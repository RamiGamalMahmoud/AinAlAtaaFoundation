using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Phone_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_phone_families_family_id",
                table: "phone");

            migrationBuilder.DropPrimaryKey(
                name: "pk_phone",
                table: "phone");

            migrationBuilder.DropIndex(
                name: "ix_phone_family_id",
                table: "phone");

            migrationBuilder.DropColumn(
                name: "family_id",
                table: "phone");

            migrationBuilder.RenameTable(
                name: "phone",
                newName: "phones");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "number",
                table: "phones",
                type: "VARCHAR(15)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_phones",
                table: "phones",
                column: "id");

            migrationBuilder.CreateTable(
                name: "family_phone",
                columns: table => new
                {
                    family_id = table.Column<int>(type: "INTEGER", nullable: false),
                    phones_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_family_phone", x => new { x.family_id, x.phones_id });
                    table.ForeignKey(
                        name: "fk_family_phone_families_family_id",
                        column: x => x.family_id,
                        principalTable: "families",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_family_phone_phone_phones_id",
                        column: x => x.phones_id,
                        principalTable: "phones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_family_phone_phones_id",
                table: "family_phone",
                column: "phones_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "family_phone");

            migrationBuilder.DropPrimaryKey(
                name: "pk_phones",
                table: "phones");

            migrationBuilder.RenameTable(
                name: "phones",
                newName: "phone");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "number",
                table: "phone",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)");

            migrationBuilder.AddColumn<int>(
                name: "family_id",
                table: "phone",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_phone",
                table: "phone",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_phone_family_id",
                table: "phone",
                column: "family_id");

            migrationBuilder.AddForeignKey(
                name: "fk_phone_families_family_id",
                table: "phone",
                column: "family_id",
                principalTable: "families",
                principalColumn: "id");
        }
    }
}
