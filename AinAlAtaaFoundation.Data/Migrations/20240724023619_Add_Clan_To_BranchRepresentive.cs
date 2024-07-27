using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Clan_To_BranchRepresentive : Migration
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
                name: "clan_id",
                table: "branch_representatives",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_branch_representatives_clan_id",
                table: "branch_representatives",
                column: "clan_id");

            migrationBuilder.AddForeignKey(
                name: "fk_branch_representatives_clans_clan_id",
                table: "branch_representatives",
                column: "clan_id",
                principalTable: "clans",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_branch_representatives_clans_clan_id",
                table: "branch_representatives");

            migrationBuilder.DropIndex(
                name: "ix_branch_representatives_clan_id",
                table: "branch_representatives");

            migrationBuilder.DropColumn(
                name: "clan_id",
                table: "branch_representatives");

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
