using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charity.Models;
using charity.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : RationalController
    {
        private readonly DataContext _db;
        private readonly ILogger<VolunteerController> _logger;

        public VolunteerController(DataContext db, ILogger<VolunteerController> logger)
        {
            _db = db;
            _logger = logger;
        }


    }
}
