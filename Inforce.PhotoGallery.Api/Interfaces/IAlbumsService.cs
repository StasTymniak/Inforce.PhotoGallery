using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Models;

namespace Inforce.PhotoGallery.Api.Interfaces
{
    public interface IAlbumsService
    {
        Task<Result> Add(string name, int? userId);
        Task<Result> Delete(int id, int userId, string? role);
        Task<Result<AlbumDTO>> GetAlbumById(int id);
        Task<Result<List<Album>>> GetAlbums();
        Task<Result<PaginatedList<AlbumDTO>>> GetAlbumsPaginated(int pageNumber, int pageSize, int? userId = null);
    }
}
