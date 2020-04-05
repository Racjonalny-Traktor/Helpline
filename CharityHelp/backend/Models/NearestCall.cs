using System;
using Microsoft.AspNetCore.Mvc;

namespace charity.Models
{
    public class NearestCall
    {
        public int Id { get; set; }

        public string DistanceString { get; set; }
        public float Distance { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}