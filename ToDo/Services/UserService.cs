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
        private readonly TokenService tokenService;

        public UserService(AppDbContext database, TokenService tokenService)
        {
            this.database = database;
            this.tokenService = tokenService;
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
        public ResponseMessage LoginUser(AuthDTO userDTO, out bool isValid)
        {
            User user = GetUserByEmail(userDTO.Email);
            if (!user.PasswordCheck(userDTO.Password))
            {
                isValid = false;
                return new ResponseMessage("Invalid credentials");
            }
            string JWT = tokenService.CreateLoginToken(user);
            isValid = true;
            return new ResponseMessage($"bearer {JWT}");
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
