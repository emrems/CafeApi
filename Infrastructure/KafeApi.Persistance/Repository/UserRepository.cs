using KafeApi.Application.Dtos.AuthDto;
using KafeApi.Application.Dtos.UserDto;
using KafeApi.Application.Interfaces;
using KafeApi.Persistance.Context.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Persistance.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;

        public UserRepository(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<userDto> CheckUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new userDto(); 
            }
            return new userDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public async Task<SignInResult> CheckUserWithPasswordAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return SignInResult.Failed;

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            return result;
        }

        public async  Task<SignInResult> LoginAsync(LoginDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email);
            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);
           
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();    
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        {
            var user = new AppIdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                PhoneNumber = dto.Phone
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            return result;

        }
    }
}
