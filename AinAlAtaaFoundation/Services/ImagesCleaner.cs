using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Services
{
    internal class ImagesCleaner(AppDbContextFactory dbContextFactory) : IImagesCleaner
    {
        public async Task CleanFamiliesIMages()
        {
            using (AppDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                IEnumerable<string> familiesImages = await dbContext
                    .Families
                    .Where(x => !string.IsNullOrEmpty(x.ImagePath))
                    .Select(x => x.ImagePath).ToListAsync();

                IEnumerable<string> storedImages = Directory.EnumerateFiles(AppService.FamiliesImagesFolder);

                storedImages
                    .Where(x => !familiesImages.Contains(x))
                    .ToList()
                    .ForEach(x => File.Delete(x));
            }
        }
    }
}
