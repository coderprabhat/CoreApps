using Microsoft.EntityFrameworkCore;
using TestDB.Models;

namespace TestDB.Db
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-one relationship
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Address)
                .WithOne(a => a.Case)
                .HasForeignKey<CaseAddress>(a => a.CaseId);

            // One-to-many relationship
            modelBuilder.Entity<Case>()
                .HasMany(c => c.Contacts)
                .WithOne(cc => cc.Case)
                .HasForeignKey(cc => cc.CaseId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Case> Cases { get; set; }

        public DbSet<CaseContact> CaseContacts { get; set; }
        public DbSet<CaseAddress> CaseAddresses { get; set; }

    }
}
