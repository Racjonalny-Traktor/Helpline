using Microsoft.EntityFrameworkCore;

namespace charity.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options = null)
        {
        }

        public DbSet<Call> Calls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=db.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Call.OnModelCreating(modelBuilder.Entity<Call>());
        }
    }
}