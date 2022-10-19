using JccApi.Entities;
using JccApi.Infrastructure.Repository;
using JccApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUsers();
            var usersResponse = users.Select(user => new GetUsersResponse
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                UserType = (Enums.UserType)user.UserTypeId,
                IsDeleted = user.IsDeleted,
            });

            return Ok(usersResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(new GetUsersResponse
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                UserType = (Enums.UserType)user.UserTypeId,
                IsDeleted = user.IsDeleted,
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel request)
        {
            var user = new User(request.Login, request.Name, request.Password, request.UserType);
            await _userRepository.Create(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequestModel request)
        {
            var user = await _userRepository.GetUserByLogin(request.Login);
            if (user is null)
            {
                return NotFound();
            }

            if (user.Password != request.Password || user.IsDeleted)
            {
                return Unauthorized();
            }

            return Ok(new AuthenticateUserResponse
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                UserType = (Enums.UserType)user.UserTypeId,
            });
        }

        [HttpPatch("{id}/password")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangeUserPasswordRequestModel request)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null)
            {
                return NotFound();
            }

            user.ChangePassword(request.NewPassword);
            await _userRepository.Update(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null)
            {
                return NotFound();
            }

            user.MarkAsDeleted();
            await _userRepository.Update(user);

            return NoContent();
        }
    }
}
