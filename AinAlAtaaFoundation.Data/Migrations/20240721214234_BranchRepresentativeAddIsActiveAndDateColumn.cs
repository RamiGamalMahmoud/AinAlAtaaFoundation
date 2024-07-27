using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class BranchRepresentativeAddIsActiveAndDateColumn : Migration
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
                defaultValue: new DateTime(2024, 7, 22, 0, 42, 34, 179, DateTimeKind.Local).AddTicks(6865),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 7, 21, 12, 52, 9, 748, DateTimeKind.Local).AddTicks(753));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "branch_representatives",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "branch_representatives",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "branch_representatives");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "branch_representatives");

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
                defaultValue: new DateTime(2024, 7, 21, 12, 52, 9, 748, DateTimeKind.Local).AddTicks(753),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 7, 22, 0, 42, 34, 179, DateTimeKind.Local).AddTicks(6865));
        }
    }
}
