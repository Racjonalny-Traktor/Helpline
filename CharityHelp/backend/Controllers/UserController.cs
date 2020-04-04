using System.Linq;
using charity.Models;
using Microsoft.AspNetCore.Mvc;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _db;

        public UserController(DataContext db)
        {
            _db = db;
        }

        [HttpPost("")]
        public IActionResult AddNewUser([FromBody] AddNewUserDto dto)
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

        //check if number exists in db xdd
        [HttpGet("{number}")]
        public IActionResult CheckUserIfExists([FromRoute] string number)
        {
            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == number);
            if (user != null)
                return Ok(new CheckIfExistsDto {Exists = true});
            return NotFound(new CheckIfExistsDto {Exists = false});
        }

        public class AddNewUserDto
        {
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public float Latitude { get; set; }
            public float Longitude { get; set; }
        }

        public class CheckIfExistsDto
        {
            public bool Exists { get; set; }
        }
    }
}