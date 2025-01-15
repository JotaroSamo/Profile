using MediatR;
using Microsoft.AspNetCore.Mvc;
using profile_Application.Profile.User.LoginUser;
using profile_MapperModel.Profile.User;

namespace profile_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
  
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
    {

        var result = await _mediator.Send(new LoginUserCommand(loginUser));
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

}