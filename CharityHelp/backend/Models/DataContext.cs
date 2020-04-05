using Microsoft.EntityFrameworkCore;

namespace charity.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<User> Users { get; set; } // elders

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(x => x.PhoneNumber);
            modelBuilder.Entity<Volunteer>().HasIndex(x => x.DeviceId);
        }
    }
}