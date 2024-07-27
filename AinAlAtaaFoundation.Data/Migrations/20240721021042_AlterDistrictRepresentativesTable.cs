using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterDistrictRepresentativesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_district_representatives_addresses_address_id",
                table: "district_representatives");

            migrationBuilder.DropIndex(
                name: "ix_district_representatives_district_id",
                table: "district_representatives");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "district_representatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 21, 5, 10, 42, 493, DateTimeKind.Local).AddTicks(4435));

            migrationBuilder.CreateIndex(
                name: "ix_district_representatives_district_id",
                table: "district_representatives",
                column: "district_id");

            migrationBuilder.AddForeignKey(
                name: "fk_district_representatives_address_address_id",
                table: "district_representatives",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_district_representatives_address_address_id",
                table: "district_representatives");

            migrationBuilder.DropIndex(
                name: "ix_district_representatives_district_id",
                table: "district_representatives");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "district_representatives");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "ix_district_representatives_district_id",
                table: "district_representatives",
                column: "district_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_district_representatives_addresses_address_id",
                table: "district_representatives",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
