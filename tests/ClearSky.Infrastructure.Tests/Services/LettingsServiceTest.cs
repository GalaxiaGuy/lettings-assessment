using System;
using System.Threading.Tasks;
using ClearSky.Infrastructure.Services;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClearSky.Infrastructure.Tests.Services
{

    public class LettingsServiceTest : LettingsDbContextBaseTest
    {
        private readonly LettingsService _service;

        public LettingsServiceTest() : base()
        {
            _service = new LettingsService(Context);
        }

        [Fact]
        public async Task FetchPropertiesFirstPageFetchesFirstProperties()
        {
            var firstProperties = await Context.Properties.AsQueryable().OrderBy(x => x.Id).Take(LettingsService.PAGE_SIZE).ToListAsync().ConfigureAwait(false);
            var firstPage = await _service.FetchPropertiesAsync(1).ToListAsync();

            Assert.True(firstProperties.SequenceEqual(firstPage));
        }

        [Fact]
        public async Task FetchPropertiesWithInterestsFirstPageFetchesFirstProperties()
        {
            var firstProperties = await Context.Properties.AsQueryable().OrderBy(x => x.Id).Take(LettingsService.PAGE_SIZE).ToListAsync().ConfigureAwait(false);
            var firstPage = await _service.FetchPropertiesWithInterestsAsync(1).ToListAsync().ConfigureAwait(false);

            Assert.True(firstProperties.SequenceEqual(firstPage));
        }

        [Fact]
        public async Task ToggleInterestCreatesInterest()
        {
            var userId = "d76c2677-dee1-48f0-bae1-52cc3f1f193e";
            var property = await Context.Properties.AsQueryable().FirstAsync().ConfigureAwait(false);

            await _service.ToggleInterestAsync(property.Id, userId).ConfigureAwait(false);

            var interest = property.Interests.SingleOrDefault(x => x.CustomerIdentityId == userId);
            Assert.NotNull(interest);
        }

        [Fact]
        public async Task ToggleInterestRemovesInterest()
        {
            var userId = "d76c2677-dee1-48f0-bae1-52cc3f1f193e";
            var property = await Context.Properties.AsQueryable().FirstAsync().ConfigureAwait(false);

            await _service.ToggleInterestAsync(property.Id, userId).ConfigureAwait(false);
            await _service.ToggleInterestAsync(property.Id, userId).ConfigureAwait(false);

            var interest = property.Interests.SingleOrDefault(x => x.CustomerIdentityId == userId);
            Assert.Null(interest);
        }

        [Fact]
        public async Task PageCountIsCorrect()
        {
            var pageCount = await _service.PageCountAsync().ConfigureAwait(false);
            var expectedPageCount = 100 / LettingsService.PAGE_SIZE;

            Assert.Equal(expectedPageCount, pageCount);
        }
    }
}
