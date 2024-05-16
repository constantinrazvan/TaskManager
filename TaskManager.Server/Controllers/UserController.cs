using Microsoft.AspNetCore.Mvc;
using TaskManager.Server.Models;
using System.Linq;

namespace TaskManager.Server.Controllers
{
    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public ActionResult NewUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Utilizatorul este gol.");
            }

            var findUser = _context.Users.FirstOrDefault(x => x.Email == user.Email);

            if (findUser != null)
            {
                return BadRequest("Adresa de email este deja in folosinta");
            }

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User newUser = new User
            {
                Email = user.Email,
                Password = passwordHashed
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser + " a fost inregistrat cu succes");
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Utilizatorul este gol.");
            }

            var findUser = _context.Users.FirstOrDefault(x => x.Email == user.Email);

            if (findUser == null)
            {
                return NotFound("Utilizatorul nu a fost gasit.");
            }

            bool checkPass = BCrypt.Net.BCrypt.Verify(user.Password, findUser.Password);

            if (!checkPass)
            {
                return BadRequest("Parolele nu corespund!");
            }

            // TODO : JWT Implementation
            return Ok("Autentificare reusita!");
        }
    }
}
