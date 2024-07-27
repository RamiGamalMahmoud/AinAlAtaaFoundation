using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class DistrictRepresentativeAddIsActiveColumn : Migration
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "district_representatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 21, 12, 52, 9, 748, DateTimeKind.Local).AddTicks(753),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 7, 21, 5, 10, 42, 493, DateTimeKind.Local).AddTicks(4435));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "district_representatives",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "district_representatives");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "district_representatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 21, 5, 10, 42, 493, DateTimeKind.Local).AddTicks(4435),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 7, 21, 12, 52, 9, 748, DateTimeKind.Local).AddTicks(753));
        }
    }
}
