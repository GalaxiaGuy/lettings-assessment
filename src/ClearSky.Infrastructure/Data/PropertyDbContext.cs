using Microsoft.EntityFrameworkCore;

namespace ClearSky.Infrastructure.Data
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options)
            : base(options)
        {
        }

        protected PropertyDbContext()
        {

        }

        public DbSet<Property> Properties { get; set; }
    }
}
