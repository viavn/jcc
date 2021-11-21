using JccApi.Entities;
using JccApi.Infrastructure.Repository;
using JccApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel request)
        {
            var user = new User(request.Login, request.Name, request.Password);
            await _userRepository.Create(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequestModel request)
        {
            var user = await _userRepository.GetUserByLogin(request.Login);
            if (user is null)
            {
                return Unauthorized();
            }

            if (user.Password != request.Password)
            {
                return Forbid();
            }

            return Ok();
        }
    }
}
