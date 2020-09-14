using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ClearSky.Infrastructure.Data
{
    public static class PropertyDbContextSeed
    {
        public static async Task CheckSeedAsync(PropertyDbContext context)
        {
            var anyProperties = await context.Properties.AnyAsync().ConfigureAwait(false);
            if (!anyProperties)
            {
                await SeedAsync(context).ConfigureAwait(false);
            }
        }

        public static async Task SeedAsync(PropertyDbContext context)
        {
            var assembly = Assembly.GetAssembly(typeof(PropertyDbContextSeed));
            using var stream = assembly.GetManifestResourceStream("ClearSky.Infrastructure.Data.seed.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync().ConfigureAwait(false);

            var seedProperties = JsonConvert.DeserializeObject<List<Property>>(json);

            for (int i = 0; i < seedProperties.Count; i++)
            {
                var property = seedProperties[i];
                var imageIndex = i % 9;
                property.ImageUrl = $"http://lorempixel.com/400/400/city/{imageIndex}/";

                // Pleasantly distributed price between 300 and 3,000
                property.ListPrice = 3 * (((1223 * i) % 900) + 100);
            }

            await context.AddRangeAsync(seedProperties).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
