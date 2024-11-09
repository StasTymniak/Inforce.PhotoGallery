
namespace Inforce.PhotoGallery.Api.Interfaces
{
    public interface IHashService
    {
        Task<string> HashPassword(string password);
    }
}
