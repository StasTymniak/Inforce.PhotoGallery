using Inforce.PhotoGallery.Api.Attribute;
using Inforce.PhotoGallery.Api.Context.Models;
using Inforce.PhotoGallery.Api.Controllers.Models;
using Inforce.PhotoGallery.Api.Interfaces;
using Inforce.PhotoGallery.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inforce.PhotoGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService _imagesService;
        private readonly IAuthService _authService;

        public ImagesController(IImagesService imagesService, IAuthService authService)
        {
            _imagesService = imagesService;
            _authService = authService;
        }

        [AuthorizeCustom]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostImageRequest request)
        {
            int? userId = _authService.TryGetUserId(base.User);

            if (!userId.HasValue)
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Not authorized"
                });
            }

            Result result = await _imagesService.Add(request.Name, request.Base64Url, request.AlbumId);

            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = result.ErrorMessage,
                    Detail = result.Exception?.Message ?? null
                });
            }

            return Ok();
        }

        [AuthorizeCustom]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            int? userId = _authService.TryGetUserId(base.User);
            string? role = _authService.TryGetUserRole(base.User);

            if (!userId.HasValue)
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Not authorized"
                });
            }

            Result result = await _imagesService.Delete(id, userId.Value, role);

            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = result.ErrorMessage,
                    Detail = result.Exception?.Message ?? null
                });
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, int albumId, int pageSize = 5)
        {

            int? userId = _authService.TryGetUserId(base.User);

            Result<PaginatedList<ImageDTO>> result = await _imagesService.GetImagesPaginated(page, albumId, pageSize, userId);

            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = result.ErrorMessage,
                    Detail = result.Exception?.Message ?? null
                });
            }

            return Ok(result.Data);
        }

        [AuthorizeCustom]
        [HttpPost("{id}/user-activity")]
        public async Task<IActionResult> PostUserActivities([FromRoute] int id, [FromBody] PostUserActivityRequest request)
        {
            int? userId = _authService.TryGetUserId(base.User);

            if (!userId.HasValue)
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Not authorized"
                });
            }

            Result result = await _imagesService.CreateUserActivity(userId.Value, id, request.IsLiked);

            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = result.ErrorMessage,
                    Detail = result.Exception?.Message ?? null
                });
            }

            return Ok();
        }

        [AuthorizeCustom]
        [HttpDelete("{id}/user-activity")]
        public async Task<IActionResult> DeleteUserActivities([FromRoute] int id)
        {
            int? userId = _authService.TryGetUserId(base.User);

            if (!userId.HasValue)
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Not authorized"
                });
            }

            Result result = await _imagesService.DeleteUserActivity(userId.Value, id);

            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = result.ErrorMessage,
                    Detail = result.Exception?.Message ?? null
                });
            }

            return Ok();
        }
    }
}
