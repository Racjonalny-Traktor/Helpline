using System;
using System.Linq;
using System.Threading.Tasks;
using charity.FCM;
using charity.Models;
using charity.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ILogger = Google.Apis.Logging.ILogger;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallController : RationalController
    {
        private readonly DataContext _db;
        private readonly ILogger<CallController> _logger;
        public CallController(DataContext db, ILogger<CallController> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        ///     for twillio microservice: call from person who need help, mobile will be automaticly notified
        /// </summary>
        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> NewCall([FromRoute] string phoneNumber)
        {
            _logger.LogInformation($"new call for number {phoneNumber}");

            var user = await _db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            if (user == null)
                return ImATeapot();

            var call = new Call
            {
                PhoneNumber = phoneNumber,
                CreatedAt = DateTime.Now,
                User = user,
                UserId = user.Id
            };
            var volunteer = await _db.Volunteers.FirstOrDefaultAsync(x => x.AssignedUserId == user.Id);
            await _db.Calls.AddAsync(call);

            await NotificationFactory.NotifyAssignedVolunteerFirst(call.ConvertCallToNearestCall(), volunteer?.DeviceId);
            await _db.SaveChangesAsync();


            return Ok(call.Id);
        }

        /// <summary>
        ///     for mobile: answer a call, "take an order etc".
        /// </summary>
        /// <returns>200 if answered, 418 if call not exists OR IS ALREADY TAKEN!</returns>
        [HttpGet("answer/{callId}")]
        public IActionResult AnswerNewCall([FromRoute] int callId)
        {
            var call = _db.Calls.Find(callId);
            if (call == null || call.IsAnswered)
                return ImATeapot();

            _logger.LogInformation($"call {callId} - {call.PhoneNumber} is answered!");
            call.IsAnswered = true;
            _db.SaveChanges();
            return Ok();
        }
    }
}