using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchPhonesTable : Migration
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
                name: "phone_id",
                table: "branch_representative",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "phone",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    number = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phone", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_branch_representative_phone_id",
                table: "branch_representative",
                column: "phone_id");

            migrationBuilder.AddForeignKey(
                name: "fk_branch_representative_phone_phone_id",
                table: "branch_representative",
                column: "phone_id",
                principalTable: "phone",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_branch_representative_phone_phone_id",
                table: "branch_representative");

            migrationBuilder.DropTable(
                name: "phone");

            migrationBuilder.DropIndex(
                name: "ix_branch_representative_phone_id",
                table: "branch_representative");

            migrationBuilder.DropColumn(
                name: "phone_id",
                table: "branch_representative");

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
