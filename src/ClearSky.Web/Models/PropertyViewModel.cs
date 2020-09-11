using System.Linq;
using ClearSky.Infrastructure.Data;

namespace ClearSky.Web.Models
{
    public class PropertyViewModel
    {
        public PropertyViewModel(Property property, string userId = null)
        {
            Id = property.Id;
            Address = property.Address;
            ImageUrl = property.ImageUrl;
            ListPrice = property.ListPrice.ToString("C");
            Description = property.Description;

            if (userId != null && property.Interests.Any(x => x.CustomerIdentityId == userId))
            {
                HasInterest = true;
            }
        }

        public string Id { get; }
        public string Address { get; }
        public string ImageUrl { get; }
        public string ListPrice { get; }
        public string Description { get; }
        public bool HasInterest { get; }
    }
}
