using AuthenticationApi.Application.Dto;
using AuthenticationApi.Domain.Enitites;
using eCommerce.SharedLibary.Reponse;
using Microsoft.AspNetCore.DataProtection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Application.Interfaces
{
    public interface IUser
    {
        public  Task<Reponse> Register(AppUserDto user);
        public  Task<Reponse> Login(LoginDto user);
        public  Task<GetUserDto> GetUser(int userId);
        public  Task<AppUser> GetUserByEmail(string Email);
    }
}
