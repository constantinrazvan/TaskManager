using Microsoft.AspNetCore.Mvc;
using TaskManager.Server.Models;
using TaskManager.Server.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Server.Controllers
{
    [ApiController]
    [Route("/api/v1/todos")]
    public class ToDoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("newTodo")]
        public ActionResult NewTodo([FromBody] NewTodoDto todo, int userId)
        {
            if (todo == null)
            {
                return BadRequest("Nu poti incarca un todo care este gol!");
            }

            // Check if the user exists
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User-ul specificat nu exista.");
            }

            Todo newTodo = new Todo
            {
                Title = todo.Title,
                IsDone = todo.IsDone,
                UserId = userId
            };

            _context.Todos.Add(newTodo);
            _context.SaveChanges();

            return Ok($"{newTodo.Title} a fost salvat in baza de date!");
        }

        [HttpGet("{id}")]
        public ActionResult GetTodoById(int id, int userId)
        {
            if (userId == 0)
            {
                return BadRequest("Id-ul tau de utilizator nu corespunde");
            }

            if (id <= 0)
            {
                return BadRequest("Din pacate nu s-a primit id-ul! Ceva nu a mers cum trebuie");
            }

            var foundTodo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (foundTodo != null)
            {
                if (foundTodo.UserId == userId)
                {
                    return Ok(foundTodo);
                }
                else
                {
                    return Unauthorized("Nu ai permisiunea de a accesa acest todo.");
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodo([FromBody] NewTodoDto todoDto, int id, int userId)
        {
            if (userId == 0)
            {
                return BadRequest("Id-ul tau de utilizator nu corespunde");
            }

            if (id <= 0)
            {
                return BadRequest("Elementul nu exista sau ceva nu a mers cum trebuie!");
            }

            var existingTodo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (existingTodo != null)
            {
                if (existingTodo.UserId != userId)
                {
                    return Unauthorized("Nu ai permisiunea de a modifica acest todo.");
                }

                existingTodo.Title = todoDto.Title;
                existingTodo.IsDone = todoDto.IsDone;

                _context.Todos.Update(existingTodo);
                _context.SaveChanges();

                return Ok($"{existingTodo.Title} a fost modificat!");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(int id, int userId)
        {
            if (userId == 0)
            {
                return BadRequest("Id-ul tau de utilizator nu corespunde");
            }

            if (id <= 0)
            {
                return BadRequest("Elementul nu exista sau ceva nu a mers cum trebuie!");
            }

            var findTodo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (findTodo != null)
            {
                if (findTodo.UserId != userId)
                {
                    return Unauthorized("Nu ai permisiunea de a sterge acest todo.");
                }

                _context.Todos.Remove(findTodo);
                _context.SaveChanges();
                return Ok($"Todo cu ID-ul {findTodo.Id} a fost sters");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("all")]
        public ActionResult<List<Todo>> GetAllTodos(int userId)
        {
            if (userId == 0)
            {
                return BadRequest("Id-ul tau de utilizator nu corespunde");
            }

            var todos = _context.Todos.Where(x => x.UserId == userId).ToList();
            return Ok(todos);
        }
    }
}
