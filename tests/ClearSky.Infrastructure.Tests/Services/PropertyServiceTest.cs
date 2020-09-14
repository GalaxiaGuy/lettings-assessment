using System;
using System.Linq;
using System.Threading.Tasks;
using ClearSky.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClearSky.Infrastructure.Tests.Services
{
    public class PropertyServiceTest : PropertyDbContextBaseTest
    {
        private readonly PropertyService _service;

        public PropertyServiceTest() : base()
        {
            _service = new PropertyService(Context);
        }

        [Fact]
        public async Task FetchPropertiesFirstPageFetchesFirstProperties()
        {
            var firstProperties = await Context.Properties.AsQueryable().OrderBy(x => x.Id).Take(PropertyService.PAGE_SIZE).ToListAsync().ConfigureAwait(false);
            var firstPage = await _service.FetchPropertiesAsync(1).ToListAsync();

            Assert.True(firstProperties.SequenceEqual(firstPage));
        }

        [Fact]
        public async Task FetchPropertiesWithInterestsFirstPageFetchesFirstProperties()
        {
            var firstProperties = await Context.Properties.AsQueryable().OrderBy(x => x.Id).Take(PropertyService.PAGE_SIZE).ToListAsync().ConfigureAwait(false);
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
            var expectedPageCount = 100 / PropertyService.PAGE_SIZE;

            Assert.Equal(expectedPageCount, pageCount);
        }
    }
}
