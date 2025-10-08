using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.TableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface ITableServices
    {
        Task<ResponseDto<List<ResultTableDto>>> GetTableList();
        Task<ResponseDto<DetailTableDto>> GetTableById(int id);
        Task<ResponseDto<DetailTableDto>> GetTableByNumber(int tableNumber);
        Task<ResponseDto<object>> CreateTable(CreateTableDto createTableDto);
        Task<ResponseDto<object>> UpdateTable(UpdateTableDto updateTableDto);
        Task<ResponseDto<object>> DeleteTable(int id);


    }
}
