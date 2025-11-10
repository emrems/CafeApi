using KafeApi.Application.Dtos.AuthDto;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Helpers;
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


        public Authservice(TokenHelpers helpers)
        {
            _helpers = helpers;
        }

        public async Task<ResponseDto<object>> GenerateTokenAsync(TokenDto dto)
        {
            try
            {
                var user = dto.Email == "admin@admin.com" ? true : false;
                if (!user)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "kullanıcı bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var token = _helpers.GenerateToken(dto);
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
