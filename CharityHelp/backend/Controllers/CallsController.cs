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

        [HttpGet("")]
        public IActionResult GetCalls()
        {
            var calls = _db.Calls
                .Where(x => !x.IsAnswered)
                .Include(x => x.User)
                .ToList();
            return Ok(calls);
        }

        [HttpPost("nearest/{maxDistance}")]
        public IActionResult GetNearestCalls([FromBody] Utils.Coords pos, [FromRoute] float maxDistance = 5.0f)
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
                .Where(x => x.Distance <= maxDistance)
                .OrderBy(x => x.Distance)
                ;//.Take(5);

            return Ok(nearest);
        }

        [HttpGet("check-number/{number}")]
        public IActionResult CheckIfExists([FromRoute] string number)
        {
            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == number);
            if (user != null)
                return Ok(new {exists = true});
            return NotFound(new {exists = false});
        }
    }
}