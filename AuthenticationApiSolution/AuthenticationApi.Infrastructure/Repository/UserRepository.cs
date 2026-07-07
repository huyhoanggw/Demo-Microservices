using AuthenticationApi.Application.Dto;
using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Domain.Enitites;
using AuthenticationApi.Infrastructure.Database;
using BCrypt.Net;
using eCommerce.SharedLibary.Reponse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Infrastructure.Repository
{
    public class UserRepository : IUser
    {
        private readonly AuthenticationDbcontext _context;
        public readonly IConfiguration _config;
        public UserRepository(AuthenticationDbcontext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<GetUserDto> GetUser(int userId)
        {
            var user = await _context.User.FindAsync(userId);
            return user is null ? null! : new GetUserDto(user.Id ,user.Name, user.TelePhone , user.Address , user.Email,user.Role );
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await _context.User.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user is null ? null! : user!;
        }

        public async Task<Reponse> Login(LoginDto user)
        {
            var getUser = await GetUserByEmail(user.Email);
            if (getUser is null) return new Reponse(false, "Invalid credentials");
            bool VerifyPassword = BCrypt.Net.BCrypt.Verify(user.Password, getUser.Password);
            if (!VerifyPassword) return new Reponse(false, "Invalid credentials");
            var token = GenerateToken(getUser);
            return new Reponse(true, token);
        }
        private  string GenerateToken(AppUser user) 
        {
            var key = Encoding.UTF8.GetBytes(_config.GetSection("Authentication:KEY").Value!);
            var securitykey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name , user.Name!),
                new(ClaimTypes.Email , user.Email!)
            };
            if (!string.IsNullOrEmpty(user.Role) || !Equals("string", user.Role)) claims.Add(new(ClaimTypes.Role, user.Role));
            var token = new JwtSecurityToken(
               issuer: _config["Authentication:Issuer"],
               audience: _config["Authentication:Audience"],
               claims: claims,
               expires: null,
               signingCredentials: credentials

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<Reponse> Register(AppUserDto user)
        {
           var getUser = await GetUserByEmail(user.Email);
            if (getUser is not null) return new Reponse(false, "you can not use this email for registration");
            var Appuser = new AppUser()
            {
                        Name = user.Name,
                TelePhone = user.TelePhone,
                Email = user.Email,
                Role = user.Role,
                Address = user.Address,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };
            var result = await _context.User.AddAsync(Appuser);
            await _context.SaveChangesAsync();
            return result.Entity.Id > 0 ? new Reponse(true, "User Registered Successfully") : new Reponse(false, "Invalid data provided");
        }
    }
}
