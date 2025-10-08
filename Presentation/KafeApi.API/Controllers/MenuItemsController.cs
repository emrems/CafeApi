using KafeApi.Application.Dtos.MenuItemDto;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemsController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
           
            var menuItems = await _menuItemServices.GetAllMenuItems();
            if (!menuItems.Success)
            {
                if (menuItems.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(menuItems);
                }
                return BadRequest(menuItems);
               
            }
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menuItem = await _menuItemServices.GetMenuItemById(id);
            if (!menuItem.Success)
            {
                if (menuItem.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(menuItem);
                }
                return BadRequest(menuItem);
            }
            return Ok(menuItem);

        }
        [HttpPost]
        public async Task<IActionResult> AddMenuItem([FromBody] CreateMenuItemDto dto)
        {
            
            var result = await _menuItemServices.AddMenuItem(dto);
            if (!result.Success)
            {
                if(result.ErrorCodes == ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
           var result= await _menuItemServices.UpdateMenuItem(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound || result.ErrorCodes == ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result= await _menuItemServices.DeleteMenuItem(id);
            if (!result.Success) 
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);

            }
            return Ok("menuItem başarıyla silindi");
        }
    }
}
