#nullable enable
using System.Security.Cryptography;

namespace ToDo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = HashPassword(value);
            }
        }
        public byte[] PasswordSalt { get; set; }
        public List<ToDoTask> Tasks { get; set; }
        public User()
        {
        }

        public User(string email, string password)
        {
            Email = email;
            PasswordSalt = RandomNumberGenerator.GetBytes(16);
            Password = password;
            Tasks = new List<ToDoTask>();
        }

        private string HashPassword(string passwordInput)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(passwordInput, PasswordSalt, 87640);
            byte[] hash = pbkdf2.GetBytes(20);

            var result = Convert.ToBase64String(hash);
            return result;
        }

        public bool PasswordCheck(string passwordInput)
        {
            return HashPassword(passwordInput) == Password;
        }
    }
}
