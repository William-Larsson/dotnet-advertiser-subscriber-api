using Microsoft.EntityFrameworkCore;
using subscribers.Models;

namespace subscribers.Data
{
    // The contet for the database. Used to create 
    // tables etc. with EF Core 
    public class ApiDBContext : DbContext
    {
        // Constructor, call superclass for setup.
        public ApiDBContext(DbContextOptions<ApiDBContext> options)
            : base(options)
        {
        }

        // DbSets are used to be able to access all the data from 
        // the database tables. Without this, data can't be added or
        // sent to each table.
        public DbSet<Subscriber> Subscribers { get; set; }   
    }
}
