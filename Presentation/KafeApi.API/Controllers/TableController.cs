using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : BaseController
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
            return CreateResponse(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var result = await _tableService.GetTableById(id);
            return CreateResponse(result);
        }

        [HttpGet("GetActiveTables")]
        public async Task<IActionResult> GetActiveTables()
        {
            var result = await _tableService.GetAllActiveTables();
            return CreateResponse(result);
        }


        [HttpGet("GetTableByNumber/{tableNumber}")]
        public async Task<IActionResult> GetTableByNumber(int tableNumber)
        {
            var result = await _tableService.GetTableByNumber(tableNumber);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable([FromBody] CreateTableDto createTableDto)
        {
            var result = await _tableService.CreateTable(createTableDto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateTableDto updateTableDto)
        {
            var result = await _tableService.UpdateTable(updateTableDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTable(id);
            return CreateResponse(result);
        }

        [HttpPatch("UpdateTableStatusById/{id}")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableService.UpdateTableStatusById(id);
            return CreateResponse(result);
        }

        [HttpPatch("UpdateTableStatusByTableNumber/{tableNumber}")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {
            var result = await _tableService.UpdateTableStatusByTableNumber(tableNumber);
            return CreateResponse(result);
        }
    }
}
