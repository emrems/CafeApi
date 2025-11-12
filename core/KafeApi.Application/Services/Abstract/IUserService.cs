using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface IUserService
    {
        Task<ResponseDto<object>> Register(RegisterDto registerDto);
        Task<ResponseDto<object>> CreateRole(string roleName);
        Task<ResponseDto<object>> AddRole(string email, string roleName);
    }
}
