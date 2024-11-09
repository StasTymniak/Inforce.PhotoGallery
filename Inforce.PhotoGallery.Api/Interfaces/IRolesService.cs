using Inforce.PhotoGallery.Api.Context.Models;

namespace Inforce.PhotoGallery.Api.Interfaces
{
    public interface IRolesService
    {
        Task<Result<Role>> GetRoleById(int id);
    }
}
