using System;
using System.Linq;
using System.Security.Cryptography;
using charity.Models;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallsController : ControllerBase
    {
        private readonly ILogger<CallsController> _logger;

        public CallsController(ILogger<CallsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetCalls()
        {
            using var db = new DataContext();
            var calls = db.Calls.Where(x => !x.IsAnswered).ToList();
            return Ok(calls);
        }

        [HttpGet("nearest")]
        public IActionResult GetNearestCalls([FromBody] LatLong pos)
        {
            //max 5km
            const float threshold = 5.0f;

            using var db = new DataContext();
            // use some kind of spatial partitioning in future
            // so no need to get all table...
            var calls = db.Calls
                .Where(x => !x.IsAnswered)
                .Include(x => x.User)
                .ToList();

            //rzeszow ofiar katynia 50.0543272, 21.9791912
            var nearest = calls
                .Select(x => ConvertCallToNearestCallDto(x, pos))
                .Where(x => x.Distance <= threshold)
                .OrderBy(x => x.Distance);

            return Ok(nearest);
        }

        private NearestCallDto ConvertCallToNearestCallDto(Call call, LatLong pos)
        {
            var p1 = new GeoCoordinate(call.User.Latitude, call.User.Longitude);
            var p2 = new GeoCoordinate(pos.Latitude, pos.Longitude);
            var dist = p1.GetDistanceTo(p2);//in meters
            dist /= 1000; // in km

            return new NearestCallDto
            {
                Latitude = call.User.Latitude,
                Longitude = call.User.Longitude,
                Distance = (float)Math.Round(dist, 1)
            };
        }

        public class NearestCallDto
        {
            public float Distance { get; set; }
            public float Latitude { get; set; }
            public float Longitude { get; set; }
        }

        public class LatLong
        {
            public float Latitude { get; set; }
            public float Longitude { get; set; }
        }
    }
}