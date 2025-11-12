using KafeApi.Application.Dtos.AuthDto;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.UserDto;
using KafeApi.Application.Helpers;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Concrete
{
    public class Authservice : IAuthService
    {
        private  readonly TokenHelpers _helpers;
        private readonly IUserRepository _userRepository;

        public Authservice(TokenHelpers helpers, IUserRepository userRepository = null)
        {
            _helpers = helpers;
            _userRepository = userRepository;
        }

        public async Task<ResponseDto<object>> GenerateTokenAsync(LoginDto dto)
         {
            try
            {
                var user = await _userRepository.CheckUserAsync(dto.Email);
                var signInResult = await _userRepository.CheckUserWithPasswordAsync(dto);
                if (user == null || !signInResult.Succeeded)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "kullanıcı bulunamadı",
                        ErrorCode = ErrorCodes.Unauthorized
                    };
                }
                var tokenDto = new TokenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role
                };

                var token = _helpers.GenerateToken(tokenDto);
                if (token == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "token oluştrulamadı",
                        ErrorCode = ErrorCodes.Exception
                    };
                }
                return new ResponseDto<object>
                {
                    Data = token,
                    Success = true,
                    Message = "token oluşturuldu",
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
