using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFamiliIsSponsered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_sponsored",
                table: "families",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_sponsored",
                table: "families");
        }
    }
}
