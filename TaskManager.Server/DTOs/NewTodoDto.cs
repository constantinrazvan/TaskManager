using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Server.DTOs
{
    public class NewTodoDto
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
    }
}
