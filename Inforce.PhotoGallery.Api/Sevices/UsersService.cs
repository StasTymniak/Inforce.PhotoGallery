using Inforce.PhotoGallery.Api.Context;
using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inforce.PhotoGallery.Api.Sevices
{
    public class UsersService : IUsersService
    {
        private readonly AppDBContext _dbContext;
        private readonly IHashService _hashService;

        public UsersService(IHashService hashService, AppDBContext appDBContext)
        {
            _hashService = hashService;
            _dbContext = appDBContext;
        }

        public async Task<Result<User>> AddUser(User user)
        {
            Result<User> result = new();
            string hashedPassword = await _hashService.HashPassword(user.Password);
            user.Password = hashedPassword;
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                result.Data = user;

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while adding the user.";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result<User>> GetUser(string username)
        {
            Result<User> result = new();

            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

                if (user == null)
                {
                    result.ErrorMessage = $"Role with username {username} not found.";

                    return result;
                }

                result.Data = user;

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while fetching the user.";
                result.Exception = ex;

                return result;
            }
        }

        public async Task<Result<User>> GetUserById(int? id)
        {
            Result<User> result = new();

            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    result.ErrorMessage = $"User with Id {id} not found.";

                    return result;
                }

                result.Data = user;

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while fetching the user.";
                result.Exception = ex;

                return result;
            }
        }
    }
}