using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using profile_Application.Chat.GetAll;
using profile_Application.Profile.User.CreateUser;
using profile_Application.Profile.User.LoginUser;
using profile_Core.Model;
using profile_Core.Password;
using profile_Core.Profile;
using profile_Domain.Profile;
using profile_MapperModel.Profile.User;

namespace profile_API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;


    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUser createUser)
    {
        var result =  await _mediator.Send(new CreateUserRequest(createUser));
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllQuery());
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}