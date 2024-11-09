using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Models;

namespace Inforce.PhotoGallery.Api.Interfaces
{
    public interface IImagesService
    {
        Task<Result> Add(string name, string url, int albumId);
        Task<Result> Delete(int id, int userId, string? role);
        Task<Result<PaginatedList<ImageDTO>>> GetImagesPaginated(int pageNumber, int albumId, int pageSize, int? userId = null);
        Task<Result> CreateUserActivity(int userId, int imageId, bool IsLiked);
        Task<Result> DeleteUserActivity(int userId, int imageId);
    }
}
