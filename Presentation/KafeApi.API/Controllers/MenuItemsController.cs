using KafeApi.Application.Dtos.MenuItemDto;
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
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menuItem = await _menuItemServices.GetMenuItemById(id);
            return Ok(menuItem);

        }
        [HttpPost]
        public async Task<IActionResult> AddMenuItem([FromBody] CreateMenuItemDto dto)
        {
            if (dto == null)
            {
                return BadRequest("MenuItem boş olamaz");
            }
            await _menuItemServices.AddMenuItem(dto);
            return StatusCode(201, "MenuItem başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            await _menuItemServices.UpdateMenuItem(dto);
            return Ok("MenuItem başarıyla güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            await _menuItemServices.DeleteMenuItem(id);
            return NoContent(); 
        }
    }
}
