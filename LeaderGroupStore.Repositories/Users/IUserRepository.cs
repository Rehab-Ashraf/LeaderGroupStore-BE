using LeaderGroupStore.Core.DomainEntities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LeaderGroupStore.Repositories.Users
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUserAsync(User user, string password, string roleId);
        Task<bool> LoginAsync(User user, string password);
        Task<User> GetUserByEmailAsync(string email);
    }
}
