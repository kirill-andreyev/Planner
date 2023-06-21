using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Azure.Identity;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces.Services;
using Constants;
using DataAccess.Models;
using DataAccess.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly JwtOptions _jwtOptions;

        public UserService(IUserRepository repository, IOptions<JwtOptions> jwtOptions)
        {
            _repository = repository;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<LoginResult> SingIn(Login user)
        {
            var dbUser = new User() { Name = user.Name, Password = PasswordEncryptor.GenerateSHA256(user.Password) };


            if (!await _repository.CheckIfUserExist(dbUser.Name))
            {
                throw new AuthenticationException("Wrong username or password");
            }

            if (!await _repository.CheckUserPassword(dbUser))
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
                Name = dbUser.Name,
                JwtToken = new JwtSecurityTokenHandler().WriteToken(jwt)
            };
        }

        public async Task<int> CreateAccount(Login user)
        {
            var dbUser = new User() { Name = user.Name, Password = PasswordEncryptor.GenerateSHA256(user.Password) };
            if (await _repository.CheckIfUserExist(user.Name))
            {
                throw new AuthenticationException("User already exist");
            }

            var id = await _repository.AddUser(dbUser);
            await _repository.Save();

            return id;
        }
    }
}
