using System;
using System.Linq;
using charity.Models;
using charity.Utils;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallsController : ControllerBase
    {
        private readonly DataContext _db;

        public CallsController(DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// for testing: get all calls
        /// </summary>
        [HttpGet("all")]
        public IActionResult GetAllCalls()
        {
            var calls = _db.Calls
                .Where(x => !x.IsAnswered)
                .Include(x => x.User)
                .ToList();
            return Ok(calls);
        }

        /// <summary>
        /// for mobile: get all nearest calls, sorted by distance
        /// </summary>
        [HttpPost("{maxDistanceInKm}")]
        public IActionResult GetNearestCalls([FromBody] Utils.Coords pos, [FromRoute] float maxDistanceInKm = 5.0f)
        {
            // use some kind of spatial partitioning in future
            // so no need to get all table...
            var calls = _db.Calls
                .Where(x => !x.IsAnswered)
                .Include(x => x.User)
                .ToList();

            //rzeszow ofiar katynia 50.0543272, 21.9791912
            var nearest = calls
                    .Select(x => x.ConvertCallToNearestCall(pos))
                    .Where(x => x.Distance <= maxDistanceInKm)
                    .OrderBy(x => x.Distance)
                ;//.Take(5);

            return Ok(nearest);
        }

    }
}