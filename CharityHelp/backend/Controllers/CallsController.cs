using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using charity.Models;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
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
                return Ok(db.Calls.ToList());
        }

        [HttpGet("testnotif")]
        public async Task<IActionResult> TestNotification()
        {
            var message = new Message
            {
                Notification = new Notification
                {
                    Title = "aaaa" + DateTime.Now.ToShortTimeString(),
                    Body = "Content for this push notification"
                },
                Topic = "help"
            };

            var result = await NotificationFactory.NotifyAsync(message);
            return Ok(result);
        }
    }
}