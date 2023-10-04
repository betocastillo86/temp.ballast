using Ballast.Application.Users.Commands.RegisterUserCommand;
using Ballast.Application.Users.Commands.ValidateLoginCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ballast.Api.Controllers;

[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] ValidateLoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}