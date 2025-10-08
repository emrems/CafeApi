using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Concrete
{
    public class TableService : ITableServices
    {
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly ITableRepository _tableRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTableDto> _tableValidator;
        private readonly IValidator<UpdateTableDto> _updateTableValidator;
        public TableService(IGenericRepository<Table> tableRepository, IMapper mapper, IValidator<CreateTableDto> tableValidator, IValidator<UpdateTableDto> updateTableValidator = null, ITableRepository tableRepo = null)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _tableValidator = tableValidator;
            _updateTableValidator = updateTableValidator;
            _tableRepo = tableRepo;
        }

        public async Task<ResponseDto<object>> CreateTable(CreateTableDto createTableDto)
        {
            try
            {
                var result = await _tableValidator.ValidateAsync(createTableDto);
                if (!result.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "doğrulama hatası",
                        ErrorCodes = string.Join(",", result.Errors.Select(x => x.ErrorMessage))
                    };
                }
                // aynı masa var olup olmadığı kontrolü
                var Checktable = await _tableRepository.GetByIdAsync(createTableDto.TableNumber);
                if(Checktable != null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message=$"{createTableDto.TableNumber} numaralı masa zaten mevcuttur",
                        ErrorCodes=ErrorCodes.DubplicateEntry
                    };
                } 
                var table = _mapper.Map<Table>(createTableDto);
                await _tableRepository.AddAsync(table);
                return new ResponseDto<object>
                {
                    Data = null,
                    Success = true,
                    Message = "masa başarılı bir şekilde oluşturuldu"
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "bir hata oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteTable(int id)
        {
            try
            {
                var tables =await _tableRepository.GetByIdAsync(id);
                if(tables is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message=$"{id} li silinecek masa bulunamadı",
                        ErrorCodes=ErrorCodes.NotFound
                    };
                }

                await _tableRepository.DeleteAsync(tables);
                return new ResponseDto<object>
                {
                    Data=null,
                    Message="masa başarılı bir şekilde silindi",
                    ErrorCodes=null,
                    Success=true
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "bir hata oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetTableById(int id)
        {
            try
            {
                var tables = await  _tableRepository.GetByIdAsync(id);
                if (tables == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "böyle bir masa bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var tableDto = _mapper.Map<DetailTableDto>(tables);
                return new ResponseDto<DetailTableDto>
                {
                    Success = true,
                    Data = tableDto,
                    Message = "masa başarılı bir şekilde çekildi"
                };
            }
            catch (Exception)
            {

                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    Message = "bir hata oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetTableByNumber(int tableNumber)
        {
            try
            {
                var table = await _tableRepo.GetByTableNumberAsync(tableNumber);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "böyle bir masa bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var tableDto = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto>
                {
                    Success = true,
                    Data = tableDto,
                    Message = "masa başarılı bir şekilde çekildi"
                };
            }
            catch (Exception)
            {

                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    Message = "bir hata oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetTableList()
        {
            try
            {
                var tables = await _tableRepository.GetAllAsync();
                if(tables == null || !tables.Any())
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "tablolar bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound,
                    };
                }
                var tableDto = _mapper.Map<List<ResultTableDto>>(tables);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Data = tableDto,
                    Message = "masalar başarılı bir şekilde çekildi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "bir hata oluştu",
                    ErrorCodes = ex.Message
                };
            }

        }

        public async Task<ResponseDto<object>> UpdateTable(UpdateTableDto updateTableDto)
        {
            try
            {
                var validatorResult = await _updateTableValidator.ValidateAsync(updateTableDto);
                if (!validatorResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(",", validatorResult.Errors.Select(x =>x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError,
                    };
                }
               // var checkTable = await _tableRepo.GetByTableNumberAsync(updateTableDto.TableNumber);
                //if (checkTable is not null)
                //{
                //    return new ResponseDto<object>
                //    {
                //        Success = false,
                //        Data = null,
                //        Message = $"{updateTableDto.TableNumber} numaralı masa zaten mevcuttur",
                //        ErrorCodes = ErrorCodes.DubplicateEntry,
                //    };
                //}
                var result = _mapper.Map<Table>(updateTableDto);
                await _tableRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "masa başarılı bir şekilde güncellendi",
                    ErrorCodes = null,
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success=false,
                    Data=null,
                    Message="bir sorun oluştu",
                    ErrorCodes = ErrorCodes.Exception,
                };
            }
            
        }
    }
}
