using System.Data.Entity;

namespace CarService
{
    public class Access : DbContext
    {
        public Access() : base("DbConnectionString") { }

        public DbSet<TypeWorksDao> TypeWorks { get; set; }
        public DbSet<BidDao> Bids { get; set; }
    }
}
