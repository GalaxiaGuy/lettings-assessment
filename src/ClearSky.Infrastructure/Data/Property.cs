using System.Collections.Generic;

namespace ClearSky.Infrastructure.Data
{
    public class Property
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public decimal ListPrice { get; set; }
        public string Description { get; set; }

        public ICollection<PropertyInterest> Interests { get; set; }        
    }        
}
