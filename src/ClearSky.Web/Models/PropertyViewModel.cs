using ClearSky.Infrastructure.Data;

namespace ClearSky.Web.Models
{
    public class PropertyViewModel
    {
        public PropertyViewModel(Property property)
        {
            Id = property.Id;
            Address = property.Address;
            ImageUrl = property.ImageUrl;
            ListPrice = property.ListPrice.ToString("C");
            Description = property.Description;
        }

        public string Id { get; }
        public string Address { get; }
        public string ImageUrl { get; }
        public string ListPrice { get; }
        public string Description { get; }
    }
}
