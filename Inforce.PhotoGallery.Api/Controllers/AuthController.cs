using Inforce.PhotoGallery.Api.Attribute;
using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Inforce.PhotoGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMemoryCache _memoryCache;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public AuthController(IAuthService authService, IMemoryCache memoryCache, ITokenBlacklistService tokenBlacklistService)
        {
            _authService = authService;
            _memoryCache = memoryCache;
            _tokenBlacklistService = tokenBlacklistService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string username, string password)
        {
            Result<string> result = await _authService.Login(username, password);

            if (result.Data == null)
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = result.ErrorMessage,
                    Detail = result.Exception?.Message ?? null
                });
            }

            return Ok(new
            {
                Token = result.Data
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] string username, string password)
        {
            Result<User> result = await _authService.Registration(username, password);

            if (result.Data == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = result.ErrorMessage,
                    Detail = result.Exception?.Message ?? null
                });
            }

            return Ok(new
            {
                User = result.Data
            });
        }

        [HttpPost("logout")]
        [AuthorizeCustom]
        public IActionResult Logout()
        {
            string token = Request.Headers.Authorization
                .ToString()
                .Replace("Bearer ", "");

            _tokenBlacklistService.BlacklistToken(token);

            return Ok(new { message = "Logged out successfully" });
        }
    }
}