using KafeApi.Application.Dtos.UserDto;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result =  await _userService.Register(dto);
            return CreateResponse(result);
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromQuery] string roleName)
        {
            var result = await _userService.CreateRole(roleName);
            return CreateResponse(result);
        }
        [HttpPost("add-role-user")]
        public async Task<IActionResult> AddRole([FromQuery] string email, [FromQuery] string roleName)
        {
            var result = await _userService.AddRole(email, roleName);
            return CreateResponse(result);
        }
    }
}
