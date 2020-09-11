using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClearSky.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClearSky.Infrastructure.Services
{
    public class LettingsService
    {
        private LettingsDbContext _lettingsDbContext;
        private const int PAGE_SIZE = 10;

        public LettingsService(LettingsDbContext lettingsDbContext)
        {
            _lettingsDbContext = lettingsDbContext;
            InitAsync();
        }

        private Task InitAsync() => LettingsDbContextSeed.CheckSeedAsync(_lettingsDbContext);

        public IAsyncEnumerable<Property> FetchPropertiesAsync(int page = 0)
        {
            return _lettingsDbContext.Properties.Skip(page * PAGE_SIZE).Take(PAGE_SIZE).AsAsyncEnumerable();
        }
    }
}
