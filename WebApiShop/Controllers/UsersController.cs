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
        public UsersController(IUsersService _usersService)
        {
            this._usersService = _usersService;
        }
        IUsersService _usersService;

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
            return NoContent();
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            user = _usersService.CreateUser(user);
            if (user == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public ActionResult<User> Post1([FromBody] User loggedUser)
        {
            User? user = _usersService.Login(loggedUser);
            if (user != null)
                return CreatedAtAction(nameof(Get), new { user.Id }, user);
            return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            try 
            {
                _usersService.UpdateUser(id, user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
