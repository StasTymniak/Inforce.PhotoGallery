namespace Inforce.PhotoGallery.Api.Models
{
    public class AlbumDTO
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int? UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Base64Url { get; set; }
    }
}
