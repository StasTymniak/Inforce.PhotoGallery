namespace Inforce.PhotoGallery.Api.Models
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Base64Url { get; set; }
        public int AlbumId { get; set; }
        public int CountLike { get; set; }
        public int CountDislike { get; set; }
        public bool? CurrentUserLiked { get; set; }
    }
}
