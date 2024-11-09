using Inforce.PhotoGallery.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inforce.PhotoGallery.Api.Context.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        public int? UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Image> Images { get; set; } = [];

        public AlbumDTO ToDtoModel()
            => new AlbumDTO
            {
                Id = Id,
                Name = Name,
                UserId = UserId,
                CreatedAt = CreatedAt,
                Base64Url = Images.FirstOrDefault()?.Base64Url ?? null,
            };
    }
}
