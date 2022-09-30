using System.Data.Entity;

namespace CarService
{
    public class Access : DbContext
    {
        public Access() : base("DbConnectionString") { }

        public DbSet<TypeWork> TypeWorks { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}
