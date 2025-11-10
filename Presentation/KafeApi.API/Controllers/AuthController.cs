using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("generate-token")] 
        public async Task<IActionResult> GenerateToken(string email)
        {
            var token = await _authService.GenerateTokenAsync(email);
            return CreateResponse(token);
        }
    }
}
