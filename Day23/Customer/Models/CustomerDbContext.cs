using Microsoft.EntityFrameworkCore;

namespace CustomerProject.Models
{
    public class CustomerDbContext:DbContext
    {
        public CustomerDbContext() { }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }

        public DbSet<Customer> Customer { get; set; }


    }
}
