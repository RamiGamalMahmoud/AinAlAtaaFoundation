using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToFamilyAndFamilyMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_delted",
                table: "family_members",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delted",
                table: "families",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_delted",
                table: "family_members");

            migrationBuilder.DropColumn(
                name: "is_delted",
                table: "families");
        }
    }
}
