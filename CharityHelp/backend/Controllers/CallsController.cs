using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charity.FCM;
using charity.Models;
using charity.Utils;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallsController : RationalController
    {
        private readonly DataContext _db;
        private readonly ILogger<CallsController> _logger;

        public CallsController(DataContext db, ILogger<CallsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        ///     for testing: get all calls
        /// </summary>
        [HttpGet("all")]
        public ActionResult<IEnumerable<Call>> GetAllCalls()
        {
            var calls = _db.Calls
                .Include(x => x.User)
                .ToList();
            return Ok(calls);
        }

        [HttpGet("{callId}")]
        public async Task Notify(int callId)
        {
            var call = _db.Calls.Find(callId);
            await NotificationFactory.NotifyAboutHelpAsync(call.ConvertCallToNearestCall());
        }

        /// <summary>
        ///     for mobile: get all nearest calls, sorted by distance, use value -1 to get all
        /// </summary>
        [HttpPost("{maxDistanceInKm}")]
        public ActionResult<IEnumerable<NearestCall>> GetNearestCalls([FromBody] Coords pos, [FromRoute] float maxDistanceInKm = -1.0f)
        {
            // use some kind of spatial partitioning in future
            // so no need to get all table...
            var calls = _db.Calls
                .Where(x => !x.IsAnswered)
                .Include(x => x.User)
                .ToList();

            //rzeszow ofiar katynia 50.0543272, 21.9791912
            var nearest = calls
                    .Select(x => maxDistanceInKm > 0 ? x.ConvertCallToNearestCall(pos) : x.ConvertCallToNearestCall())
                    .Where(x => !(maxDistanceInKm > 0) || x.Distance <= maxDistanceInKm)
                    .OrderBy(x => x.Distance)
                ; //.Take(5);

            return Ok(nearest);
        }
    }
}