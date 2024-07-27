using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    deletable_model_deleted_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deletable_model_is_deleted = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: false),
                    user_name = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    is_admin = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    password = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "is_admin", "password", "user_name" },
                values: new object[] { 1, true, "admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "ix_users_user_name",
                table: "users",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
