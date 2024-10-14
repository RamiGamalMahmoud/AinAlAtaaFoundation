using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHasFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "has_flag",
                table: "families",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "has_flag",
                table: "families");
        }
    }
}
