using System.Threading.Tasks;
using ClearSky.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClearSky.Infrastructure.Tests
{

    public abstract class LettingsDbContextBaseTest
    {
        protected LettingsDbContext Context { get; }

        public LettingsDbContextBaseTest()
        {
            Context = new LettingsTestDbContext();
            new DbContextOptionsBuilder<LettingsDbContext>();
            InitDbAsync(Context).GetAwaiter().GetResult();
        }

        private async Task InitDbAsync(LettingsDbContext context)
        {
            await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
            await context.Database.MigrateAsync().ConfigureAwait(false);
            await LettingsDbContextSeed.CheckSeedAsync(context).ConfigureAwait(false);
        }
    }
}
