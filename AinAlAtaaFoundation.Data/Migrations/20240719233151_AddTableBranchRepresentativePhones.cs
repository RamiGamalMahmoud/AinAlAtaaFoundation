using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableBranchRepresentativePhones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_branch_representatives_phone_phone_id",
                table: "branch_representatives");

            migrationBuilder.DropIndex(
                name: "ix_branch_representatives_phone_id",
                table: "branch_representatives");

            migrationBuilder.DropColumn(
                name: "phone_id",
                table: "branch_representatives");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "branch_representative_phone",
                columns: table => new
                {
                    branch_representative_id = table.Column<int>(type: "INTEGER", nullable: false),
                    phones_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_branch_representative_phone", x => new { x.branch_representative_id, x.phones_id });
                    table.ForeignKey(
                        name: "fk_branch_representative_phone_branch_representatives_branch_representative_id",
                        column: x => x.branch_representative_id,
                        principalTable: "branch_representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_branch_representative_phone_phone_phones_id",
                        column: x => x.phones_id,
                        principalTable: "phone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_branch_representative_phone_phones_id",
                table: "branch_representative_phone",
                column: "phones_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "branch_representative_phone");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "phone_id",
                table: "branch_representatives",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_branch_representatives_phone_id",
                table: "branch_representatives",
                column: "phone_id");

            migrationBuilder.AddForeignKey(
                name: "fk_branch_representatives_phone_phone_id",
                table: "branch_representatives",
                column: "phone_id",
                principalTable: "phone",
                principalColumn: "id");
        }
    }
}
