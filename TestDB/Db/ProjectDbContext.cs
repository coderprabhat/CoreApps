using Microsoft.EntityFrameworkCore;
using TestDB.Models;

namespace TestDB.Db
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<TestDB.Models.Case> Cases { get; set; } = default!;

    }
}
