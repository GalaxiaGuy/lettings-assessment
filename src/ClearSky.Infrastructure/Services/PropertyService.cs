using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClearSky.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClearSky.Infrastructure.Services
{
    public class PropertyService
    {
        private PropertyDbContext _propertyDbContext;
        public const int PAGE_SIZE = 10;

        public PropertyService(PropertyDbContext propertyDbContext)
        {
            _propertyDbContext = propertyDbContext;
        }

        public IAsyncEnumerable<Property> FetchPropertiesAsync(int page)
            => _propertyDbContext.Properties.OrderBy(x => x.Id).Skip((page-1) * PAGE_SIZE).Take(PAGE_SIZE).AsAsyncEnumerable();

        public IAsyncEnumerable<Property> FetchPropertiesWithInterestsAsync(int page)
            => _propertyDbContext.Properties.OrderBy(x => x.Id).Include(x => x.Interests).Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).AsAsyncEnumerable();

        public async Task<bool> ToggleInterestAsync(string propertyId, string userId)
        {
            var property = await _propertyDbContext.Properties.Include(x => x.Interests).SingleAsync(x => x.Id == propertyId).ConfigureAwait(false);
            var interest = property.Interests.SingleOrDefault(x => x.CustomerIdentityId == userId);

            var hasInterest = false;
            if (interest == null)
            {
                _propertyDbContext.Add(new PropertyInterest { CustomerIdentityId = userId, Property = property });
                hasInterest = true;
            }
            else
            {
                property.Interests.Remove(interest);
            }
            await _propertyDbContext.SaveChangesAsync().ConfigureAwait(false);
            return hasInterest;
        }

        public async Task<int> PageCountAsync()
            => (await _propertyDbContext.Properties.CountAsync()) / PAGE_SIZE ;
    }
}
