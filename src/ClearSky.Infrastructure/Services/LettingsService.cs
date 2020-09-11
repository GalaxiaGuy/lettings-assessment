﻿using System.Collections.Generic;
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

        public IAsyncEnumerable<Property> FetchPropertiesAsync(int page)
            => _lettingsDbContext.Properties.Skip((page-1) * PAGE_SIZE).Take(PAGE_SIZE).AsAsyncEnumerable();

        public IAsyncEnumerable<Property> FetchPropertiesWithInterestsAsync(int page)
            => _lettingsDbContext.Properties.Include(x => x.Interests).Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).AsAsyncEnumerable();

        public async Task ToggleInterestAsync(string propertyId, string userId)
        {
            var property = await _lettingsDbContext.Properties.Include(x => x.Interests).SingleAsync(x => x.Id == propertyId).ConfigureAwait(false);
            var interest = property.Interests.SingleOrDefault(x => x.CustomerIdentityId == userId);

            if (interest == null)
            {
                _lettingsDbContext.Add(new PropertyInterest { CustomerIdentityId = userId, Property = property });
            }
            else
            {
                property.Interests.Remove(interest);
            }
            await _lettingsDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> PageCountAsync()
            => (await _lettingsDbContext.Properties.CountAsync()) / PAGE_SIZE + 1;
    }
}
