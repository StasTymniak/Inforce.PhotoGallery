namespace Inforce.PhotoGallery.Api.Controllers.Models
{
    public class PostImageRequest
    {
        public string Name { get; set; }
        public string Base64Url { get; set; }
        public int AlbumId { get; set; }
    }
}
