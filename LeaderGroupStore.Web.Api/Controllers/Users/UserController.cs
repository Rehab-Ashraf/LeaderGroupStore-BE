using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using LeaderGroupStore.Models.User;
using AutoMapper;
using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Services.Users;
using LeaderGroupStore.Models.Users;

namespace LeaderGroupStore.Web.Api.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UserController(
            IConfiguration config,
            IUserService userService,
            IMapper mapper
            )
        {
            this.mapper = mapper;
            this.userService = userService;
            this.config = config;
        }

        [HttpPost]
        [Route("User/Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]  RegisterInputModel model)
        {
            var user = mapper.Map<User>(model);
            var result = await userService.RegisterAsync(user, model.Password,model.RoleId);

            if (result.Succeeded)
            {
                return Ok( "User created successfully!" );
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Errors", error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("User/Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInputModel model)
        {
            var result = await userService.LoginAsync(model.UserName, model.Password);

            if (result == null)
            {
                return BadRequest("Incorrect Username or Password");
            }

            return Ok( result );
            
        }

    }
}
