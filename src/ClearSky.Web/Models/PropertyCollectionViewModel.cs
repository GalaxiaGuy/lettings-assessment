using System.Collections.Generic;

namespace ClearSky.Web.Models
{
    public class PropertyCollectionViewModel : PagedCollectionViewModel
    {
        public PropertyCollectionViewModel(IAsyncEnumerable<PropertyViewModel> properties, int currentPage, int pageCount)
            :base(currentPage, pageCount)
        {
            Properties = properties;
        }

        public IAsyncEnumerable<PropertyViewModel> Properties { get; }
    }
}
