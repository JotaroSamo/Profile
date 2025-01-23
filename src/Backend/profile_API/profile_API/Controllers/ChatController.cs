using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profile_Application.Chat.CreateChat;
using profile_Application.Chat.GetChats;
using profile_Core.Contracts;
using profile_MapperModel.Profile.Chat;

namespace profile_API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextService _httpContextService;

    public ChatController(IMediator mediator, IHttpContextService httpContextService)
    {
        _mediator = mediator;
        _httpContextService = httpContextService;
    }
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateChat createChat)
    {
        var userId = _httpContextService.GetCurrentUserGuid().Value;
        if (userId == Guid.Empty)
            return BadRequest();
        createChat.UsersIds.Add(_httpContextService.GetCurrentUserGuid().Value);
        var result = await _mediator.Send(new CreateChatCommand(createChat));
        return Ok(result);
        
    }
    [HttpGet("get-user-chats")]
    public async Task<IActionResult> Get()
    {
        var userId = _httpContextService.GetCurrentUserGuid().Value;
        if (userId == Guid.Empty)
            return BadRequest();
        var result = await _mediator.Send(new GetChatQuery(userId));
        
        return Ok(result);
        
    }
}