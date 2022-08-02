using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDo.Models;
using ToDo.Models.DTOs;

namespace ToDo.Services
{
    public class TokenService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserService userService;

        public TokenService(IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }

        public string CreateLoginToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Future Password is Incoming"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public User GetLoggedInUser()
        {
            var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return userService.GetUserById(int.Parse(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value));
            }
            return null;
        }
        public ResponseMessage LoginUser(AuthDTO userDTO, out bool isValid)
        {
            User user = userService.GetUserByEmail(userDTO.Email);
            if (!user.PasswordCheck(userDTO.Password))
            {
                isValid = false;
                return new ResponseMessage("Invalid credentials");
            }
            string JWT = CreateLoginToken(user);
            isValid = true;
            return new ResponseMessage($"bearer {JWT}");
        }
    }
}

