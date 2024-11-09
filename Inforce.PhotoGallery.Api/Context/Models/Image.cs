using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inforce.PhotoGallery.Api.Models;

namespace Inforce.PhotoGallery.Api.Context.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        public required string Base64Url { get; set; }

        public int AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public Album? Album { get; set; } = null;
        public ICollection<UserActivity> UserActivities { get; set; } = [];

        public ImageDTO ToDomainModel(int? userId = null)
            => new ImageDTO
            {
                Id = Id,
                Name = Name,
                Base64Url = Base64Url,
                AlbumId = AlbumId,
                CountLike = UserActivities
                    .Where(us => us.IsLiked)
                    .Count(),
                CountDislike = UserActivities
                    .Where(us => !us.IsLiked)
                    .Count(),
                CurrentUserLiked = userId.HasValue 
                    ? UserActivities
                        .FirstOrDefault(us => userId.Value == us.UserId)
                        ?.IsLiked ?? null
                    : null
            };
    }
}
