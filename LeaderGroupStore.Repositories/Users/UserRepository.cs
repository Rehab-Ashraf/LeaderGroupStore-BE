using LeaderGroupStore.Core.DomainEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LeaderGroupStore.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly LeaderGroupStore_dbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(LeaderGroupStore_dbContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string password, string roleId)
        {
            var result = await _userManager.CreateAsync(user, password);
            var getrole = await _roleManager.FindByIdAsync(roleId);
            if (getrole != null)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, getrole.Name);

                if (isAdmin == false)
                {
                    await _userManager.AddToRoleAsync(user, getrole.Name);
                }

            }

            return result;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user;
        }

        public async Task<IList<string>> GetUserRoleAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<bool> LoginAsync(User user, string password)
        {
            bool isRightPassword = await _userManager.CheckPasswordAsync(user, password);
            return isRightPassword;
        }
    }
}
