using Microsoft.EntityFrameworkCore;
using advertising.Models;

namespace advertising.Data
{
    // The applications database context, used to 
    // access EF Core operations on each table. 
    public class AppDBContext : DbContext
    {
        public AppDBContext (DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Data annotations does not seem to work for the FK,
            // so I changed the column name like this instead. 
            modelBuilder.Entity<Ad>()
                .Property(x => x.AdvertiserId)
                .HasColumnName("ad_fk_advertiser_id");
        }

        // DbSets are used to be able to access all the data from 
        // the database tables. Without this, data can't be added or
        // sent to each table.
        public DbSet<Advertiser> Advertisers { get; set; }

        public DbSet<Ad> Ads { get; set; }
    }
    
}