using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBranchRepresentativesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_branch_representative_branches_branch_id",
                table: "branch_representative");

            migrationBuilder.DropForeignKey(
                name: "fk_branch_representative_phone_phone_id",
                table: "branch_representative");

            migrationBuilder.DropPrimaryKey(
                name: "pk_branch_representative",
                table: "branch_representative");

            migrationBuilder.RenameTable(
                name: "branch_representative",
                newName: "branch_representatives");

            migrationBuilder.RenameIndex(
                name: "ix_branch_representative_phone_id",
                table: "branch_representatives",
                newName: "ix_branch_representatives_phone_id");

            migrationBuilder.RenameIndex(
                name: "ix_branch_representative_branch_id",
                table: "branch_representatives",
                newName: "ix_branch_representatives_branch_id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_branch_representatives",
                table: "branch_representatives",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_branch_representatives_branches_branch_id",
                table: "branch_representatives",
                column: "branch_id",
                principalTable: "branches",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_branch_representatives_phone_phone_id",
                table: "branch_representatives",
                column: "phone_id",
                principalTable: "phone",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_branch_representatives_branches_branch_id",
                table: "branch_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_branch_representatives_phone_phone_id",
                table: "branch_representatives");

            migrationBuilder.DropPrimaryKey(
                name: "pk_branch_representatives",
                table: "branch_representatives");

            migrationBuilder.RenameTable(
                name: "branch_representatives",
                newName: "branch_representative");

            migrationBuilder.RenameIndex(
                name: "ix_branch_representatives_phone_id",
                table: "branch_representative",
                newName: "ix_branch_representative_phone_id");

            migrationBuilder.RenameIndex(
                name: "ix_branch_representatives_branch_id",
                table: "branch_representative",
                newName: "ix_branch_representative_branch_id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_branch_representative",
                table: "branch_representative",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_branch_representative_branches_branch_id",
                table: "branch_representative",
                column: "branch_id",
                principalTable: "branches",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_branch_representative_phone_phone_id",
                table: "branch_representative",
                column: "phone_id",
                principalTable: "phone",
                principalColumn: "id");
        }
    }
}
