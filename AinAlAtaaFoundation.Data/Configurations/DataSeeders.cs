using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class DistrictsSeeder : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasData(new[]
            {
                new { Id = 1, Name = "الجزيرة/ الجزء الشمالي"},
                new { Id = 2, Name = "الملحة"},
                new { Id = 3, Name = "أبو تونية"},
                new { Id = 4, Name = "الموالي"},
                new { Id = 5, Name = "الأبيتر"},
                new { Id = 6, Name = "الطريشة"},
                new { Id = 7, Name = "تل الكورة"},
                new { Id = 8, Name = "تل العورة"},
                new { Id = 9, Name = "بنات الحسن"},
                new { Id = 10, Name = "القادسية"},
                new { Id = 11, Name = "حليحل وسالم"},
                new { Id = 12, Name = "الضباعي"},
                new { Id = 13, Name = "البلدية (القاطول)"},
                new { Id = 14, Name = "المنظمة"},
                new { Id = 15, Name = "البو رحمن"},
                new { Id = 16, Name = "الصحن (الإمام)"},
                new { Id = 17, Name = "المدرسة الأولى"},
                new { Id = 18, Name = "الزراعة"},
                new { Id = 19, Name = "الجيل الثائر"},
                new { Id = 20, Name = "السكك"},
                new { Id = 21, Name = "حي المعتصم"},
                new { Id = 22, Name = "حي مدرسة المعتصم"},
                new { Id = 23, Name = "حي المستشفى"},
                new { Id = 24, Name = "حي الهادي"},
                new { Id = 25, Name = "المتوكلية"},
                new { Id = 26, Name = "معمل الأدوية"},
                new { Id = 27, Name = "حي الضباط"},
                new { Id = 28, Name = "حي العرموشية الأولى"},
                new { Id = 29, Name = "حي القادسية"},
                new { Id = 30, Name = "العرموشية الثانية"},
                new { Id = 31, Name = "حي المعلمين"},
                new { Id = 32, Name = "حي الجبيرية"},
                new { Id = 33, Name = "حي الخضراء"},
                new { Id = 34, Name = "حي الشهداء"},
                new { Id = 35, Name = "حي الإفراز"},
                new { Id = 36, Name = "حي المثنى"},
                new { Id = 37, Name = "حي صلاح الدين"},
                new { Id = 38, Name = "حي الشرطة"},
                new { Id = 39, Name = "حي القلعة"},
                new { Id = 40, Name = "حي الكهرومائية"},
                new { Id = 41, Name = "حي دور السكك"},
                new { Id = 42, Name = "حي الزهور"},
                new { Id = 43, Name = "حي الصعيوية"},
                new { Id = 44, Name = "حي الشيخ رياح"},
                new { Id = 45, Name = "حي مكيشيفة"},

            });
        }
    }

    internal class ClansSeeder : IEntityTypeConfiguration<Clan>
    {
        public void Configure(EntityTypeBuilder<Clan> builder)
        {
            builder.HasData(new[]
            {
                new { Id = 1, Name = "البو عباس" },
                new { Id = 2, Name = "البو باز" },
                new { Id = 3, Name = "البو أسود" },
                new { Id = 4, Name = "البو نيسان" },
                new { Id = 5, Name = "البو بدري" },
                new { Id = 6, Name = "البو دراج" },
                new { Id = 7, Name = "البو عيسى" },
            });
        }
    }

    internal class GenderSeeder : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(new[]
            {
                new { Id = 1, NameAr = "ذكر"},
                new { Id = 2, NameAr = "أنثى"}
            });
        }
    }
}
