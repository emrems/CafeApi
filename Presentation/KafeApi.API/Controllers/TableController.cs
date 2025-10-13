using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
      private readonly ITableServices _tableService;

        public TableController(ITableServices tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTableList()
        {
            var result = await _tableService.GetTableList();
            if (!result.Success)
            {
                if(result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var result = await _tableService.GetTableById(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetActiveTables")]
        public async Task<IActionResult> GetActiveTables()
        {
            var result = await _tableService.GetAllActiveTables();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("GetTableByNumber/{tableNumber}")]
        public async Task<IActionResult> GetTableByNumber(int tableNumber)
        {
            var result = await _tableService.GetTableByNumber(tableNumber);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable([FromBody] CreateTableDto createTableDto)
        {
            var result = await _tableService.CreateTable(createTableDto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.DubplicateEntry)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateTableDto updateTableDto)
        {
            var result = await _tableService.UpdateTable(updateTableDto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTable(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("UpdateTableStatusById/{id}")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableService.UpdateTableStatusById(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("UpdateTableStatusByTableNumber/{tableNumber}")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {
            var result = await _tableService.UpdateTableStatusByTableNumber(tableNumber);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
