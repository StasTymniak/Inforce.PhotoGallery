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
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsService _albumService;
        private readonly IAuthService _authService;

        public AlbumsController(IAlbumsService albumService, IAuthService authService)
        {
            _albumService = albumService;
            _authService = authService;
        }

        [AuthorizeCustom]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostAlbumRequest request)
        {
            int? userId = _authService.TryGetUserId(base.User);

            if (!userId.HasValue)
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Not authorized"
                });
            }

            Result result = await _albumService.Add(request.AlbumName, userId);

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

            Result result = await _albumService.Delete(id, userId.Value, role);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Result<AlbumDTO> result = await _albumService.GetAlbumById(id);

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
        [HttpGet("user")]
        public async Task<IActionResult> GetAllByUser([FromQuery] int page, int pageSize = 5)
        {
            int? userId = _authService.TryGetUserId(base.User);

            if (!userId.HasValue)
            {
                return Unauthorized(new ProblemDetails
                {
                    Title = "Not authorized"
                });
            }
            Result<PaginatedList<AlbumDTO>> result = await _albumService.GetAlbumsPaginated(page, pageSize, userId);

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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, int pageSize = 5)
        {
            Result<PaginatedList<AlbumDTO>> result = await _albumService.GetAlbumsPaginated(page, pageSize, null);

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
    }
}
