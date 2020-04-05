﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace charity.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public User AssignedUser { get; set; }
        public int AssignedUserId { get; set; }
    }
}
