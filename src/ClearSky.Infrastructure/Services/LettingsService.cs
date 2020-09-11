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

        public IAsyncEnumerable<Property> FetchPropertiesAsync(int page = 1)
            => _lettingsDbContext.Properties.Skip((page-1) * PAGE_SIZE).Take(PAGE_SIZE).AsAsyncEnumerable();

        public async Task<int> PageCountAsync()
            => (await _lettingsDbContext.Properties.CountAsync()) / PAGE_SIZE + 1;
    }
}
