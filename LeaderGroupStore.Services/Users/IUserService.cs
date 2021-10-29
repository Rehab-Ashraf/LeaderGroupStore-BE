

using System.Threading.Tasks;
using LeaderGroupStore.Core.DomainEntities;
using Microsoft.AspNetCore.Identity;

namespace LeaderGroupStore.Services.Users
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(User model, string password, string roleId);
        Task<string> LoginAsync(string email, string password);
    }
}
