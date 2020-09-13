using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClearSky.Infrastructure.Data
{
    public class LettingsDbContext : DbContext
    {
        public LettingsDbContext(DbContextOptions<LettingsDbContext> options)
            : base(options)
        {
        }

        protected LettingsDbContext()
        {

        }

        public DbSet<Property> Properties { get; set; }
    }
}
