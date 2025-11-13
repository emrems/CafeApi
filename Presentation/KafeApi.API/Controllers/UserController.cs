using KafeApi.Application.Dtos.UserDto;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize(Roles = "Admin")]
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromQuery] string roleName)
        {
            var result = await _userService.CreateRole(roleName);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-role-user")]
        public async Task<IActionResult> AddRole([FromQuery] string email, [FromQuery] string roleName)
        {
            var result = await _userService.AddRole(email, roleName);
            return CreateResponse(result);
        }
    }
}
