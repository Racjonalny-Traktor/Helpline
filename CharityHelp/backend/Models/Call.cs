using System;

namespace charity.Models
{
    public class Call
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAnswered { get; set; }
        public DateTime CalledAt { get; set; }
    }
}