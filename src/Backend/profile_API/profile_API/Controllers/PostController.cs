using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profile_Application.Profile.Post;
using profile_Application.Profile.Post.CreatePost;
using profile_Application.Profile.Post.GetUserPost;
using profile_Core.Contracts;
using profile_MapperModel.Profile.Post;

namespace profile_API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextService _httpContextService;

    public PostController(IMediator mediator, IHttpContextService httpContextService)
    {
        _mediator = mediator;
        _httpContextService = httpContextService;
    }

    [HttpGet("user-posts")]
    public async Task<IActionResult> GetPosts()
    {
        var publicID = _httpContextService.GetCurrentUserGuid();
        if (publicID == null)
        {
            return BadRequest("Not authorized");
        }
        var result = await _mediator.Send(new GetUserPostsQuery(publicID.Value));
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreatePost post)
    {
        var publicID = _httpContextService.GetCurrentUserGuid();
        if (publicID == null)
        {
            return BadRequest("Not authorized");
        }
        var result = await _mediator.Send(new CreatePostCommand(post, publicID.Value));
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }
    
}