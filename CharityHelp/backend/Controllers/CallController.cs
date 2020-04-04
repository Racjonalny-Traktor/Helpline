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
    public class CallController : ControllerBase
    {
        private readonly DataContext _db;

        public CallController(DataContext db)
        {
            _db = db;
        }

        //all fucking async xdd
        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> NewCall([FromRoute] string phoneNumber)
        {
            var user = await _db.Users.FirstAsync(x => x.PhoneNumber == phoneNumber);
            var call = new Call
            {
                PhoneNumber = phoneNumber,
                CalledAt = DateTime.Now,
                User = user,
                UserId = user.Id
            };
            await _db.Calls.AddAsync(call);
            await _db.SaveChangesAsync();

            await NotificationFactory.NotifyAboutHelpAsync(call.ConvertCallToNearestCall());

            return Ok(call.Id);
        }

        [HttpGet("answer/{callId}")]
        public IActionResult DebugAnswerNewCall([FromRoute] int callId)
        {
            _db.Calls.Find(callId).IsAnswered = true;
            _db.SaveChanges();
            return Ok();
        }
    }
}