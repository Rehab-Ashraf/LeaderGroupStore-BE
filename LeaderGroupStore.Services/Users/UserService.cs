using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LeaderGroupStore.Services.Users
{
    public class UserService:IUserService
    {
        private readonly IUserRepository userRepo;
        private readonly IConfiguration configuration;
        public UserService(IUserRepository userRepo, IConfiguration configuration)
        {
            this.userRepo = userRepo;
            this.configuration = configuration;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await userRepo.GetUserByEmailAsync(email);
            var userRole = await userRepo.GetUserRoleAsync(user);
            if (user == null)
                return null;

            bool isRightPassword = await userRepo.LoginAsync(user, password);
            if (isRightPassword)
            {
                string bearerToken = GenerateToken(user,userRole);
                return bearerToken;
            }

            return null;
        }

        public async Task<string> Logout()
        {
           return await userRepo.Logout();
        }

        public async Task<IdentityResult> RegisterAsync(User model, string password, string roleId)
        {
            var result = await userRepo.AddUserAsync(model, password, roleId);
            return result;
        }

        private  string GenerateToken(User user, IList<string> roleName)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                            new Claim(nameof(user.Id),user.Id),
                            new Claim("Roles", roleName[0]),
                            new Claim(ClaimTypes.Name, user.UserName),
                        }),

                Issuer = configuration["IdentitySettings:Issuer"],
                Audience = configuration["IdentitySettings:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["IdentitySettings:TokenExpiryInMinutes"].ToString())),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["IdentitySettings:SecurityKey"])), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var bearerToken = tokenHandler.WriteToken(securityToken);

            return bearerToken;


        }
    }
}
