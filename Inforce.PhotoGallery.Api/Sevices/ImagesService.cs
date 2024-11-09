using Inforce.PhotoGallery.Api.Context;
using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Interfaces;
using Inforce.PhotoGallery.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Inforce.PhotoGallery.Api.Sevices
{
    public class ImagesService : IImagesService
    {
        private readonly AppDBContext _dbContext;
        private readonly IAlbumsService _albumsService;

        public ImagesService(AppDBContext appDBContext, IAlbumsService albumsService)
        {
            _dbContext = appDBContext;
            _albumsService = albumsService;
        }

        public async Task<Result> Add(string name, string url, int albumId)
        {
            Result result = new();

            Image image = new Image
            {
                Name = name,
                Base64Url = url,
                AlbumId = albumId,
            };

            try
            {
                await _dbContext.Images.AddAsync(image);
                await _dbContext.SaveChangesAsync();


                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while adding the Image.";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result> Delete(int id, int userId, string? role)
        {
            Result result = new();

            try
            {
                var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == id);

                if (image == null)
                {
                    result.ErrorMessage = "An error occure while fetching image";

                    return result;
                }

                var album = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == image.AlbumId);

                if (album == null)
                {
                    result.ErrorMessage = "An error occure while fetching album";

                    return result;
                }


                if (album.UserId != userId && role != "Admin")
                {
                    result.ErrorMessage = "You have no permission to delete this image";

                    return result;
                }

                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occure while deleting an image";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result<PaginatedList<ImageDTO>>> GetImagesPaginated(int pageNumber, int albumId, int pageSize, int? userId = null)
        {
            Result<PaginatedList<ImageDTO>> result = new();

            try
            {
                var query = _dbContext.Images
                    .Where(x => x.AlbumId == albumId)
                    .AsQueryable();

                var totalItems = await query.CountAsync();

                var items = await query
                    .Include(x => x.UserActivities)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(i => i.ToDomainModel(userId))
                    .ToListAsync();

                result.Data = new PaginatedList<ImageDTO>
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
                result.ErrorMessage = "An error occurred while fetching the Images.";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result> CreateUserActivity(int userId, int imageId, bool IsLiked)
        {
            Result result = new();

            UserActivity userActivity = new()
            {
                UserId = userId,
                ImageId = imageId,
                IsLiked = IsLiked
            };

            await this.DeleteUserActivity(userId, imageId);

            try
            {
                _dbContext.UserActivities.Add(userActivity);
                await _dbContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occure while adding an user activity";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result> DeleteUserActivity(int userId, int imageId)
        {
            Result result = new();

            try
            {
                var userActivity = await _dbContext.UserActivities
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.ImageId == imageId);

                if (userActivity == null)
                {
                    result.ErrorMessage = "An error occure while fetching user activity";

                    return result;
                }

                _dbContext.UserActivities.Remove(userActivity);
                await _dbContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occure while deleting user activity";
                result.Exception = ex;

                return result;
            }
        }

    }
}
