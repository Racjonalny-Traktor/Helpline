using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace charity.Models
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Call> Calls { get; set; }
        public DbSet<User> Users { get; set; } // elders
    }
}