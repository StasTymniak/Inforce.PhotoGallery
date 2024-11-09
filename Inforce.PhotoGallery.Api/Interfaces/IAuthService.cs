using Inforce.PhotoGallery.Api.Context.Models;
using System.Security.Claims;

namespace Inforce.PhotoGallery.Api.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> Login(string username, string password);
        Task<Result<User>> Registration(string username, string password);
        int? TryGetUserId(ClaimsPrincipal userPrincipal);
        string? TryGetUserRole(ClaimsPrincipal userPrincipal);
    }
}
