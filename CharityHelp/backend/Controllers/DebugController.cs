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
        private readonly DataContext _db;

        public DebugController(DataContext db)
        {
            _db = db;
        }

        [HttpPost("debug-answer-call")]
        public IActionResult DebugAnswerNewCall([FromBody] DebugAnswerNewCallDto dto)
        {
            _db.Calls.Find(dto.Id).IsAnswered = true;
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost("debug-add-new-call")]
        public IActionResult DebugAddNewCall([FromBody] DebugAddNewCallDto dto)
        {
            var user = _db.Users.First(x => x.PhoneNumber == dto.PhoneNumber);
            var call = new Call
            {
                PhoneNumber = dto.PhoneNumber, 
                CalledAt = DateTime.Now, 
                User = user, 
                UserId = user.Id
            };
            _db.Calls.Add(call);
            _db.SaveChanges();
            return Ok(call.Id);
        }

        [HttpPost("debug-add-new-user")]
        public IActionResult DebugAddNewUser([FromBody] DebugAddNewUserDto dto)
        {
            var user = new User
            {
                Address = dto.Address, 
                Latitude = dto.Latitude, 
                Longitude = dto.Longitude, 
                PhoneNumber = dto.PhoneNumber
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(user.Id);
        }

        public class DebugAnswerNewCallDto
        {
            public int Id { get; set; }
        }

        public class DebugAddNewCallDto
        {
            public string PhoneNumber { get; set; }
        }

        public class DebugAddNewUserDto
        {
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public float Latitude { get; set; }
            public float Longitude { get; set; }
        }

    }
}
