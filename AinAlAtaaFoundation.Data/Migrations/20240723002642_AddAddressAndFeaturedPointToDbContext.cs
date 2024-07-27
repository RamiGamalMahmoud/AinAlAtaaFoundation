using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressAndFeaturedPointToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_addresses_featured_point_featured_point_id",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "fk_district_representatives_address_address_id",
                table: "district_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_families_address_address_id",
                table: "families");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "fk_addresses_featured_points_featured_point_id",
                table: "addresses",
                column: "featured_point_id",
                principalTable: "featured_points",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_district_representatives_addresses_address_id",
                table: "district_representatives",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_families_addresses_address_id",
                table: "families",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_addresses_featured_points_featured_point_id",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "fk_district_representatives_addresses_address_id",
                table: "district_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_families_addresses_address_id",
                table: "families");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "fk_addresses_featured_point_featured_point_id",
                table: "addresses",
                column: "featured_point_id",
                principalTable: "featured_points",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_district_representatives_address_address_id",
                table: "district_representatives",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_families_address_address_id",
                table: "families",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
