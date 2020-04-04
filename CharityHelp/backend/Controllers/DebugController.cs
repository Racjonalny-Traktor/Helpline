using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charity.Models;
using Microsoft.AspNetCore.Mvc;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DebugController : ControllerBase
    {
        [HttpPost("debug-answer-call")]
        public IActionResult DebugAnswerNewCall([FromBody] DebugAnswerNewCallDto dto)
        {
            using var db = new DataContext();
            db.Calls.Find(dto.Id).IsAnswered = true;
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("debug-add-new-call")]
        public IActionResult DebugAddNewCall([FromBody] DebugAddNewCallDto dto)
        {
            using var db = new DataContext();
            var call = new Call { PhoneNumber = dto.PhoneNumber, CalledAt = DateTime.Now};
            db.Calls.Add(call);
            db.SaveChanges();
            return Ok(call.Id);
        }

        public class DebugAnswerNewCallDto { public int Id { get; set; } }
        public class DebugAddNewCallDto { public string PhoneNumber { get; set; } }

    }
}
