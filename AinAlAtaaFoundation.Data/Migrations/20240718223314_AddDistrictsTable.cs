using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AinAlAtaaFoundation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDistrictsTable : Migration
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
                name: "districts",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_districts", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "districts",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "الجزيرة/ الجزء الشمالي" },
                    { 2, "الملحة" },
                    { 3, "أبو تونية" },
                    { 4, "الموالي" },
                    { 5, "الأبيتر" },
                    { 6, "الطريشة" },
                    { 7, "تل الكورة" },
                    { 8, "تل العورة" },
                    { 9, "بنات الحسن" },
                    { 10, "القادسية" },
                    { 11, "حليحل وسالم" },
                    { 12, "الضباعي" },
                    { 13, "البلدية (القاطول)" },
                    { 14, "المنظمة" },
                    { 15, "البو رحمن" },
                    { 16, "الصحن (الإمام)" },
                    { 17, "المدرسة الأولى" },
                    { 18, "الزراعة" },
                    { 19, "الجيل الثائر" },
                    { 20, "السكك" },
                    { 21, "حي المعتصم" },
                    { 22, "حي مدرسة المعتصم" },
                    { 23, "حي المستشفى" },
                    { 24, "حي الهادي" },
                    { 25, "المتوكلية" },
                    { 26, "معمل الأدوية" },
                    { 27, "حي الضباط" },
                    { 28, "حي العرموشية الأولى" },
                    { 29, "حي القادسية" },
                    { 30, "العرموشية الثانية" },
                    { 31, "حي المعلمين" },
                    { 32, "حي الجبيرية" },
                    { 33, "حي الخضراء" },
                    { 34, "حي الشهداء" },
                    { 35, "حي الإفراز" },
                    { 36, "حي المثنى" },
                    { 37, "حي صلاح الدين" },
                    { 38, "حي الشرطة" },
                    { 39, "حي القلعة" },
                    { 40, "حي الكهرومائية" },
                    { 41, "حي دور السكك" },
                    { 42, "حي الزهور" },
                    { 43, "حي الصعيوية" },
                    { 44, "حي الشيخ رياح" },
                    { 45, "حي مكيشيفة" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "districts");

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
