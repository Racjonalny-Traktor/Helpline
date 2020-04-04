using System;
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
    }

    public class Call
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAnswered { get; set; }
        public DateTime CalledAt { get; set; }
    }
}