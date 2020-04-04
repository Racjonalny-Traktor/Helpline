using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace charity.Models
{
    public class Call
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAnswered { get; set; }
        public DateTime CalledAt { get; set; }

        public static void OnModelCreating(EntityTypeBuilder<Call> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.PhoneNumber).IsRequired();
            entity.Property(x => x.CalledAt).ValueGeneratedOnAdd();
        }
    }
}