using Inforce.PhotoGallery.Api.Context.Models;

namespace Inforce.PhotoGallery.Api.Interfaces
{
    public interface IUsersService
    {
        Task<Result<User>> AddUser(User user);
        Task<Result<User>> GetUser(string username);
        Task<Result<User>> GetUserById(int? id);
    }
}
