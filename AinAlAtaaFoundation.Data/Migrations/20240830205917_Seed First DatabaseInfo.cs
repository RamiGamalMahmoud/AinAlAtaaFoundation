using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFirstDatabaseInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "database_infos",
                columns: new[] { "id", "date_updated", "discreption", "version" },
                values: new object[] { 1, new DateTime(2024, 8, 30, 11, 50, 0, 0, DateTimeKind.Unspecified), "Initial Database Schema", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "database_infos",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
