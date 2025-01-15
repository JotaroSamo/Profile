using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using profile_Application.Chat.GetAll;
using profile_Application.Profile.User.CreateUser;
using profile_Application.Profile.User.LoginUser;
using profile_Application.Profile.User.Query.FindUsers;
using profile_Core.Model;
using profile_Core.Password;
using profile_Core.Profile;
using profile_Domain.Profile;
using profile_MapperModel.Profile.User;

namespace profile_API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;


    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUser createUser)
    {
        var result =  await _mediator.Send(new CreateUserCommand(createUser));
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }
    [AllowAnonymous]
    [HttpGet("find-users")]
    public async Task<IActionResult> FindUsers([FromQuery] string login)
    {
        var result =  await _mediator.Send(new FindUsersQuery(login));
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }
   
}