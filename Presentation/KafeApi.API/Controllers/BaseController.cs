using KafeApi.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult CreateResponse<T>(ResponseDto<T> response) where T : class
        {
            if (response.Success)
            {
                return Ok(response);
            }
            return response.ErrorCode switch
            {
                ErrorCodes.NotFound => NotFound(response),
                ErrorCodes.Unauthorized => Unauthorized(response),
                ErrorCodes.Forbidden => StatusCode(403, response),
                ErrorCodes.ValidationError => BadRequest(response),
                ErrorCodes.DubplicateEntry => Conflict(response),
                ErrorCodes.Exception => StatusCode(500, response),
                ErrorCodes.BadRequest => BadRequest(response),
                _ => BadRequest(response),
            };

        }
    }
}
