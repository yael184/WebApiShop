using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Text.Json;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IPasswordsService _passwordsService;

        public UsersController(IUsersService usersService, IPasswordsService passwordsService)
        {
            _usersService = usersService;
            _passwordsService = passwordsService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User? user = _usersService.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            int passwordLevel = _passwordsService.passwordValidation(user.Password);
            if (passwordLevel < 3)
                return BadRequest($"Password too weak (score: {passwordLevel}/4). Minimum required: 3");

            user = _usersService.CreateUser(user);
            if (user == null)
                return BadRequest("Failed to create user");
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public ActionResult<User> Post1([FromBody] User loggedUser)
        {
            User? user = _usersService.Login(loggedUser);
            if (user != null)
                return Ok(user);
            return Unauthorized("Invalid username or password");
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            int passwordLevel = _passwordsService.passwordValidation(user.Password);
            if (passwordLevel < 3)
                return BadRequest($"Password too weak (score: {passwordLevel}/4). Minimum required: 3");

            _usersService.UpdateUser(id, user);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
