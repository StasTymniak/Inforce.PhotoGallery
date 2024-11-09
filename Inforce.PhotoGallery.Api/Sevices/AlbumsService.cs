using Inforce.PhotoGallery.Api.Context;
using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Interfaces;
using Inforce.PhotoGallery.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Inforce.PhotoGallery.Api.Sevices
{
    public class AlbumsService : IAlbumsService
    {
        private readonly AppDBContext _dbContext;
        private readonly IUsersService _usersService;

        public AlbumsService(AppDBContext appDBContext, IUsersService usersService)
        {
            _dbContext = appDBContext;
            _usersService = usersService;
        }

        public async Task<Result> Add(string name, int? userId)
        {
            Result result = new();

            Album album = new Album
            {
                Name = name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            try
            {
                await _dbContext.Albums.AddAsync(album);
                await _dbContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while adding the Album.";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result> Delete(int id, int userId, string? role)
        {
            Result result = new();

            try
            {
                var album = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == id);

                if (album == null)
                {
                    result.ErrorMessage = "An error occure while fetching album";

                    return result;
                }

                if (album.UserId != userId && role != "Admin")
                {
                    result.ErrorMessage = "You have no permission to delete this album";

                    return result;
                }

                _dbContext.Albums.Remove(album);
                await _dbContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Ad error occure while deleting an album";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result<AlbumDTO>> GetAlbumById(int id)
        {
            Result<AlbumDTO> result = new();

            try
            {
                var album = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == id);

                if (album == null)
                {
                    result.ErrorMessage = $"Album with Id {id} not found.";

                    return result;
                }

                result.Data = album.ToDtoModel();

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while fetching the album.";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result<PaginatedList<AlbumDTO>>> GetAlbumsPaginated(int pageNumber, int pageSize, int? userId = null)
        {
            Result<PaginatedList<AlbumDTO>> result = new();

            try
            {
                var query = _dbContext.Albums
                    .Where(x => !userId.HasValue || x.UserId == userId)
                    .AsQueryable();

                var totalItems = await query.CountAsync();

                var items = await query
                    .Include(x => x.Images)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => x.ToDtoModel())
                    .ToListAsync();

                result.Data = new PaginatedList<AlbumDTO>
                {
                    Items = items,
                    TotalItems = totalItems,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while fetching the Albums.";
                result.Exception = ex;

                return result;
            }

        }

        public async Task<Result<List<Album>>> GetAlbums()
        {
            Result<List<Album>> result = new();

            try
            {
                result.Data = await _dbContext.Albums.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while fetching the Albums.";
                result.Exception = ex;

                return result;
            }
        }
    }

}

