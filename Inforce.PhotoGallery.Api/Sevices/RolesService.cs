using Inforce.PhotoGallery.Api.Context;
using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inforce.PhotoGallery.Api.Sevices
{
    public class RolesService : IRolesService
    {
        private readonly AppDBContext _dbContext;

        public RolesService(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<Result<Role>> GetRoleById(int id)
        {
            Result<Role> result = new();
            
            try
            {
                var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);

                if (role == null)
                {
                    result.ErrorMessage = $"Role with Id {id} not found.";

                    return result;
                }

                result.Data = role;

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while fetching the role.";
                result.Exception = ex;

                return result;
            }
        }
    }
}