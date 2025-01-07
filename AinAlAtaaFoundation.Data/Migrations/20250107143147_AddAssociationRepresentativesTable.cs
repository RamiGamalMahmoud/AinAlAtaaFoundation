using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssociationRepresentativesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "association_representative_id",
                table: "families",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "association_representatives",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_association_representatives", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_families_association_representative_id",
                table: "families",
                column: "association_representative_id");

            migrationBuilder.AddForeignKey(
                name: "fk_families_association_representatives_association_representative_id",
                table: "families",
                column: "association_representative_id",
                principalTable: "association_representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_families_association_representatives_association_representative_id",
                table: "families");

            migrationBuilder.DropTable(
                name: "association_representatives");

            migrationBuilder.DropIndex(
                name: "ix_families_association_representative_id",
                table: "families");

            migrationBuilder.DropColumn(
                name: "association_representative_id",
                table: "families");
        }
    }
}
