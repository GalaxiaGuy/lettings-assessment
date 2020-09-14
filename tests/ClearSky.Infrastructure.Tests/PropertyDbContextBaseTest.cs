using System.Threading.Tasks;
using ClearSky.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClearSky.Infrastructure.Tests
{

    public abstract class PropertyDbContextBaseTest
    {
        protected PropertyDbContext Context { get; }

        public PropertyDbContextBaseTest()
        {
            Context = new PropertyTestDbContext();
            new DbContextOptionsBuilder<PropertyDbContext>();
            InitDbAsync(Context).GetAwaiter().GetResult();
        }

        private async Task InitDbAsync(PropertyDbContext context)
        {
            await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
            await context.Database.MigrateAsync().ConfigureAwait(false);
            await PropertyDbContextSeed.CheckSeedAsync(context).ConfigureAwait(false);
        }
    }
}
