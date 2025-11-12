using FluentValidation;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.UserDto;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
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

        public UserService(IUserRepository userRepository, IValidator<RegisterDto> registerDtoValidator)
        {
            _userRepository = userRepository;
            _registerDtoValidator = registerDtoValidator;
        }

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
                if(result.Succeeded)
                {
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Message = "Kayıt işlemi başarılı.",
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
