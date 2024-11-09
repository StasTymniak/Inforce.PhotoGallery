using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inforce.PhotoGallery.Api.Sevices
{
    public class AuthService : IAuthService
    {

        private readonly IHashService _hashService;
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;

        public AuthService(IHashService hashService, IUsersService usersService, IRolesService rolesService)
        {
            _hashService = hashService;
            _usersService = usersService;
            _rolesService = rolesService;
        }

        public async Task<Result<string>> Login(string username, string password)
        {
            Result<string> result = new();

            Result<User> user = await _usersService.GetUser(username);

            if (user == null)
            {
                result.ErrorMessage = "Invalid Username or password";
                return result;
            }

            string hashedPassword = await _hashService.HashPassword(password);

            if (user.Data.Password != hashedPassword)
            {
                result.ErrorMessage = "Invalid Username or password";
                return result;
            }

            Result<string> token = await GenerateJwt(user.Data);
            result.Data = token.Data;

            return result;
        }

        public async Task<Result<User>> Registration(string username, string password)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                RoleId = 1
            };

            Result<User> result = await _usersService.AddUser(user);

            return result;
        }

        private string? TryGetUserClaim(ClaimsPrincipal userPrincipal, string claimType)
             => userPrincipal != null && userPrincipal.Identity != null && userPrincipal.Identity.IsAuthenticated
                 ? userPrincipal.FindFirst(claimType)?.Value ?? null
                 : null;

        public int? TryGetUserId(ClaimsPrincipal userPrincipal)
            => int.TryParse(this.TryGetUserClaim(userPrincipal, ClaimTypes.NameIdentifier), out int userId)
                ? userId
                : null;

        public string? TryGetUserRole(ClaimsPrincipal userPrincipal)
            => this.TryGetUserClaim(userPrincipal, ClaimTypes.Role);

        private async Task<Result<string>> GenerateJwt(User user)
        {
            Result<string> result = new();

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var key = Encoding.UTF8.GetBytes("your-256-bit-long-secret-key-here-32-bytes");

            var identity = new ClaimsIdentity(
            [
                //new Claim("sub" ,$"{user.Id}"),
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(ClaimTypes.Name, $"{user.Username}")
            ]);

            if (user.RoleId.HasValue)
            {
                Result<Role> resultUserRole = await _rolesService.GetRoleById(user.RoleId.Value);

                if (resultUserRole.Data == null)
                {
                    result.ErrorMessage = resultUserRole.ErrorMessage;
                    result.Exception = resultUserRole.Exception;

                    return result;
                }

                identity.AddClaim(new Claim(ClaimTypes.Role, resultUserRole.Data.Name));
            }

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(600),
                SigningCredentials = credentials,
                Issuer = "your-issuer",
                Audience = "your-audience"
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            result.Data = jwtTokenHandler.WriteToken(token);
            return result;
        }
    }
}
