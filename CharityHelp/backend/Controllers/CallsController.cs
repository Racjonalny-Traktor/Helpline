using System.Linq;
using charity.Models;
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
            var calls = db.Calls.Where(x => !x.IsAnswered).ToList();
            return Ok(calls);
        }
    }
}