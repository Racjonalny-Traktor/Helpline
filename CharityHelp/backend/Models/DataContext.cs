using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace charity.Models
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(x => x.PhoneNumber);
        }

        public DbSet<Call> Calls { get; set; }
        public DbSet<User> Users { get; set; } // elders
    }
}