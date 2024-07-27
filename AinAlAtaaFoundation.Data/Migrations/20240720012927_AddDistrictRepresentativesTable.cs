using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDistrictRepresentativesTable : Migration
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

            migrationBuilder.CreateTable(
                name: "district_representatives",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    district_id = table.Column<int>(type: "INTEGER", nullable: false),
                    address_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_district_representatives", x => x.id);
                    table.ForeignKey(
                        name: "fk_district_representatives_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_district_representatives_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "district_representative_phone",
                columns: table => new
                {
                    district_representative_id = table.Column<int>(type: "INTEGER", nullable: false),
                    phones_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_district_representative_phone", x => new { x.district_representative_id, x.phones_id });
                    table.ForeignKey(
                        name: "fk_district_representative_phone_district_representatives_district_representative_id",
                        column: x => x.district_representative_id,
                        principalTable: "district_representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_district_representative_phone_phone_phones_id",
                        column: x => x.phones_id,
                        principalTable: "phone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_district_representative_phone_phones_id",
                table: "district_representative_phone",
                column: "phones_id");

            migrationBuilder.CreateIndex(
                name: "ix_district_representatives_address_id",
                table: "district_representatives",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_district_representatives_district_id",
                table: "district_representatives",
                column: "district_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "district_representative_phone");

            migrationBuilder.DropTable(
                name: "district_representatives");

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
