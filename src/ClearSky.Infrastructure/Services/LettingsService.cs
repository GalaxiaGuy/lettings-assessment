using System;
using System.Threading.Tasks;
using ClearSky.Infrastructure.Data;

namespace ClearSky.Infrastructure.Services
{
    public class LettingsService
    {
        private LettingsDbContext _lettingsDbContext;

        public LettingsService(LettingsDbContext lettingsDbContext)
        {
            _lettingsDbContext = lettingsDbContext;
            InitAsync();
        }

        public Task InitAsync() => LettingsDbContextSeed.CheckSeedAsync(_lettingsDbContext);
    }
}
