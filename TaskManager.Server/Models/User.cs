namespace TaskManager.Server.Models
{
    public class User
    {
        private int id;
        private string email; 
        private string password;

        public User () { }

        public int Id { get { return id; } set {  id = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }
    }
}
