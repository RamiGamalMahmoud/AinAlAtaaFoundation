using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class FeaturedPointAddDistrictId : Migration
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
                defaultValue: new DateTime(2024, 7, 22, 13, 6, 2, 487, DateTimeKind.Local).AddTicks(8372),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 7, 22, 0, 42, 34, 179, DateTimeKind.Local).AddTicks(6865));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                defaultValue: new DateTime(2024, 7, 22, 0, 42, 34, 179, DateTimeKind.Local).AddTicks(6865),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 7, 22, 13, 6, 2, 487, DateTimeKind.Local).AddTicks(8372));
        }
    }
}
