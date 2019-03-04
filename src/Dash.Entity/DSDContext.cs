using Dash.Entity.Objects;
using Microsoft.EntityFrameworkCore;

namespace Dash.Entity
{
    public class DSDContext : DbContext
    {
        public DSDContext(DbContextOptions<DSDContext> options) : base(options)
        {   
        }

        public DbSet<DSDUser> Users { get; set; }
        public DbSet<DSDSite> Sites { get; set; }
        public DbSet<DSDAccessibleSite> AccessibleSites { get; set; }
        public DbSet<DSDDataShare> DataShares { get; set; }
    }
}