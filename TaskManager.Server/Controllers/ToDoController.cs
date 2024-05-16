using Microsoft.AspNetCore.Mvc;
using TaskManager.Server.Models;

namespace TaskManager.Server.Controllers
{
    [ApiController]
    [Route("/api/v1/todos")]
    public class ToDoController : Controller
    {
        private readonly AppDbContext _context;

        public ToDoController(AppDbContext context) { 
            this._context = context;
        }

        [HttpPost("newTodo")]
        public ActionResult NewTodo([FromBody] Todo todo)
        {
            if(todo == null)
            {
                return BadRequest("Nu poti incarca un todo care este gol!");
            }
            else
            {
                Todo newTodo = new Todo
                {
                    Title = todo.Title,
                    IsDone = todo.IsDone,
                };

                _context.Todos.Add(newTodo);
                _context.SaveChanges();

                return Ok(newTodo + " a fost salvat in baza de date!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetTodoById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Din pacate nu s-a primit id-ul! Ceva nu a mers cum trebuie");
            }

            var foundTodo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (foundTodo != null)
            {
                return Ok(foundTodo);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodo([FromBody] Todo todo, int id)
        {
            if(id <= 0)
            {
                return BadRequest("Elementul nu exista sau ceva nu a mers cum trebuie!");
            }

            var existingTodo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if(existingTodo != null) 
            {
                existingTodo.Title = todo.Title;
                existingTodo.IsDone = todo.IsDone;

                _context.Todos.Update(existingTodo);
                _context.SaveChanges();

                return Ok(existingTodo + " a fost modificat!");
            } else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(int id) 
        {
            if(id  <= 0)
            {
                return BadRequest("Elementul nu exista sau ceva nu a mers cum trebuie!");
            }

            var findTodo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if(findTodo != null)
            {
                _context.Todos.Remove(findTodo);
                return Ok("Todo cu ID-ul " + findTodo.Id + " a fost sters");
            } else
            {
                return NotFound();
            }
        }

        [HttpGet("all")]
        public ActionResult<List<Todo>> GetAllTodos()
        {
            List<Todo> todos = _context.Todos.ToList();
            return Ok(todos);
        }
    }
}
