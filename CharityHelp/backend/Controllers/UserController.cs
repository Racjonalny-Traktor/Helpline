using System.ComponentModel.DataAnnotations;
using System.Linq;
using charity.Models;
using charity.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace charity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : RationalController
    {
        private readonly DataContext _db;
        private readonly ILogger<UserController> _logger;

        public UserController(DataContext db, ILogger<UserController> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        ///     for twillio microservice: create new user
        /// </summary>
        /// <returns>AddNewUserDto</returns>
        [HttpPost("")]
        public ActionResult<AddNewUserDto> AddNewUser([FromBody] AddNewUserDto dto)
        {
            _logger.LogInformation($"creating new user: {dto.PhoneNumber} - {dto.Address}");
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

        /// <summary>
        ///     for twillio microservice: check if number exists in database
        /// </summary>
        [HttpGet("{number}")]
        public ActionResult<CheckIfExistsDto> CheckUserIfExists([FromRoute] string number)
        {
            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == number);
            return Ok(new CheckIfExistsDto {Exists = user != null});
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