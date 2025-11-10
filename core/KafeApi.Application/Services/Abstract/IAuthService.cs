using KafeApi.Application.Dtos.AuthDto;
using KafeApi.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface IAuthService
    {
        Task<ResponseDto<object>> GenerateTokenAsync(TokenDto dto);
    }
}
