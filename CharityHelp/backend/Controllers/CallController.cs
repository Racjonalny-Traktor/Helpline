using System;
using System.Threading.Tasks;
using charity.Models;
using charity.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallController : RationalController
    {
        private readonly DataContext _db;

        public CallController(DataContext db)
        {
            _db = db;
        }

        /// <summary>
        ///     for twillio microservice: call from person who need help, mobile will be automaticly notified
        /// </summary>
        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> NewCall([FromRoute] string phoneNumber)
        {
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
            await _db.Calls.AddAsync(call);

            await NotificationFactory.NotifyAboutHelpAsync(call.ConvertCallToNearestCall());
            await _db.SaveChangesAsync();


            return Ok(call.Id);
        }

        /// <summary>
        ///     for mobile: answer a call, "take an order etc".
        /// </summary>
        [HttpGet("answer/{callId}")]
        public IActionResult AnswerNewCall([FromRoute] int callId)
        {
            var call = _db.Calls.Find(callId);
            if (call == null || call.IsAnswered)
                return ImATeapot();
            call.IsAnswered = true;
            _db.SaveChanges();
            return Ok();
        }
    }
}