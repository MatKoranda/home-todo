#nullable enable
using Microsoft.EntityFrameworkCore;
using ToDo.Database;
using ToDo.Models;
using ToDo.Models.DTOs;

namespace ToDo.Services
{
    public class UserService
    {
        private AppDbContext database { get; set; }

        public UserService(AppDbContext database)
        {
            this.database = database;
        }
        public ResponseMessage RegisterUser(AuthDTO authDTO, out bool isValid)
        {

            if (GetUserByEmail(authDTO.Email) != null)
            {
                isValid = false;
                return new ResponseMessage("Email is already registered");
            }
            User user = new User(authDTO.Email, authDTO.Password);
            database.Users.Add(user);
            database.SaveChanges();
            isValid = true;
            return new ResponseMessage("User was successfuly registered");
        }
        

        public User GetUserById(int userId)
        {
            return database.Users.Include(u => u.Tasks).FirstOrDefault(u => u.Id == userId);
        }
        public User GetUserByEmail(string userEmail)
        {
            return database.Users.Include(u => u.Tasks).FirstOrDefault(u => u.Email == userEmail);
        }
    }
}
