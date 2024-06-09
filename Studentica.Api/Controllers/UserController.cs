using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studentica.Api.Services;
using Studentica.Common.DTOs.Requests.User;
using Studentica.Common.DTOs.User;

namespace Studentica.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService<Guid> _userService;
        public UserController(IUserService<Guid> userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId:guid}")]
        [Authorize]
        public async Task<ActionResult<UserDto<Guid>>> GetByIdAsync(Guid userId)
        {
            return await _userService.Get(userId);
        }

        [HttpGet]
        [Authorize]
        public async Task<IReadOnlyCollection<UserDto<Guid>>> GetAllAsync([FromQuery] int count = int.MaxValue, [FromQuery] string? searchQuery=null)
        {
            return await _userService.GetAllAsync(count, searchQuery);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto<Guid>>> PostAsync(UserCreateRequest request)
        {
            var user = await _userService.Create(request, HttpContext);

            var actionName = nameof(GetByIdAsync);

            return CreatedAtAction(actionName, new { userId = user.Id }, user);
        }
    }
}
