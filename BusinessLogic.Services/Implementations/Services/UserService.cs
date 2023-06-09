﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces.Services;
using Constants;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtOptions _jwtOptions;

        public UserService(ApplicationDbContext context, IOptions<JwtOptions> jwtOptions)
        {
            _context = context;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<LoginResult> SingIn(Login user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(a => a.Name == user.Name);
            user.Password = PasswordEncryptor.GenerateSHA256(user.Password);

            if (dbUser == null || dbUser.Password != user.Password)
            {
                throw new AuthenticationException("Wrong username or password");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name)
            };
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                    SecurityAlgorithms.HmacSha256));

            return new LoginResult
            {
                Id = dbUser.Id, 
                Name = dbUser.Name,
                JwtToken = new JwtSecurityTokenHandler().WriteToken(jwt)
            };
        }

        public async Task CreateAccount(Login user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(a => a.Name == user.Name);
            if (dbUser != null)
            {
                throw new AuthenticationFailedException("User already exist");
            }

            dbUser = new User
                { Name = user.Name, Password = PasswordEncryptor.GenerateSHA256(user.Password)};
            await _context.AddAsync(dbUser);
            await _context.SaveChangesAsync();
        }
    }
}
