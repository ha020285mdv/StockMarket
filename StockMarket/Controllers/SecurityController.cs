using System.Data;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Application.Commands;
using StockMarket.Application.Exceptions;
using StockMarket.Application.Queries;
using StockMarket.Database.Model;

namespace StockMarket.Controllers;

[Route("api/v1/securyties")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly IMediator _mediator;

    public SecurityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SecurityEntity>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> ListPersons([FromQuery] SecuritiesQuery query)
    {
        var persons = await _mediator.Send(query);
        return Ok(persons);
    }

    [HttpGet("{ticker}")]
    [ProducesResponseType(typeof(SecurityEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetSecyrity(string ticker)
    {
        try
        {
            var person = await _mediator.Send(new SecurityQuery() { Ticker = ticker });
            return Ok(person);
        }
        catch (SecurityNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(SecurityEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateSecyrity(CreateSecurityCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Created("api/v1/securyties/" + command.Ticker, null);
        }
        catch (SecurityAlreadyExistsException)
        {
            return Conflict();
        }
    }

    [HttpDelete("{ticker}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteSecurity(string Ticker)
    {
        try
        {
            await _mediator.Send(new DeleteSecurityCommand() { Ticker = Ticker });
            return Ok();
        }
        catch (SecurityNotFoundException)
        {
            return NotFound();
        }
    }

}