namespace TaskManager.Server.Models
{
    public class Todo
    {
        private int id;
        private string title;
        private bool isDone; 

        public Todo() { }

        public int Id { get { return id; } set {  id = value; } }
        public string Title { get { return title; } set { title = value; } }
        public bool IsDone { get {  return isDone; } set {  isDone = value; } }
    }
}
