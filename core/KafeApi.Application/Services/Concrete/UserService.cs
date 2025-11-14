using FluentValidation;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.UserDto;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Concrete
{
   
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterDto> _registerDtoValidator;
        private readonly UserManager<User> _userManager;
     //   private readonly RoleManager<> _roleManager;

        public UserService(IUserRepository userRepository, IValidator<RegisterDto> registerDtoValidator)
        {
            _userRepository = userRepository;
            _registerDtoValidator = registerDtoValidator;
           
            
        }

        public async Task<ResponseDto<object>> AddRole(string email, string roleName)
        {
            try
            {
                var result = await _userRepository.AddRoleToUserAsync(email, roleName);
                if (result)
                {
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Message = "Kullanıcıya rol ekleme işlemi başarılı.",
                        Data = null,
                        ErrorCode = null
                    };
                }
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Kullanıcıya rol ekleme işlemi başarısız.",
                    Data = null,
                    ErrorCode = ErrorCodes.BadRequest
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Kullanıcıya rol ekleme işlemi sırasında bir hata oluştu.",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> CreateRole(string roleName)
        {
            try
            {
                var result =await _userRepository.CreateRoleAsync(roleName);
                if (result)
                {
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Message = "Rol oluşturma işlemi başarılı.",
                        Data = null,
                        ErrorCode = null
                    };
                }
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Rol oluşturma işlemi başarısız.",
                    Data = null,
                    ErrorCode = ErrorCodes.BadRequest
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Rol oluşturma işlemi sırasında bir hata oluştu.",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }



        // register olurken role atama işlemi de yapılıyor ancak yapılmamali
        public async Task<ResponseDto<object>> Register(RegisterDto registerDto)
        {
            try
            {
                var validationRegisterDtoResult = await _registerDtoValidator.ValidateAsync(registerDto);
                if (!validationRegisterDtoResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Kayıt işlemi başarısız.",
                        Data = null,
                        ErrorCode = string.Join(", ", validationRegisterDtoResult.Errors.Select(e => e.ErrorMessage).ToList())
                    };
                }
                var result = await _userRepository.RegisterAsync(registerDto);
                var resultRole = await _userRepository.AddRoleToUserAsync(registerDto.Email, "Customer");
                if (result.Succeeded)
                {
                    if(resultRole == false)
                    {
                        return new ResponseDto<object>
                        {
                            Success = false,
                            Message = "Kayıt işlemi başarılı ancak varsayılan rol ataması başarısız.",
                            Data = null,
                            ErrorCode = ErrorCodes.ValidationError
                        };
                    }


                    return new ResponseDto<object>
                    {
                        Success = true,
                        Message = "Kayıt ve rol atama  işlemi başarılı.",
                        Data = null,
                        ErrorCode = null
                    };
                }
               
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Kayıt işlemi başarısız.",
                    Data = null,
                    ErrorCode = string.Join(", ", errors)
                };
                
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Kayıt işlemi sırasında bir hata oluştu.",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
