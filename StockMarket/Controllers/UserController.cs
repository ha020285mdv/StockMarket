using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Application.Commands;
using StockMarket.Application.Exceptions;
using StockMarket.Application.Queries;
using StockMarket.Database.Model;

namespace StockMarket.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(UserEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUser(string email)
    {
        try
        {
            var user = await _mediator.Send(new UserQuery() { Email = email.ToLower() });
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        try
        {
            var user = await _mediator.Send(command);
            return Ok(user);
        }
        catch (UserAlreadyExistsException)
        {
            return Conflict();
        }
    }
}
