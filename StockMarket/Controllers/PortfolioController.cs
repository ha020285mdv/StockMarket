using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Application.Commands;
using StockMarket.Application.Exceptions;
using StockMarket.Application.Queries;
using StockMarket.Database.Model;

namespace StockMarket.Controllers;

[Route("api/v1/portfolios")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly IMediator _mediator;

    public PortfolioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(UserPortfolioEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> ListPortfolios([FromQuery] string? ticker)
    {
        try
        {
            var portfolios = await _mediator.Send(new PortfolioQuery() { Ticker = ticker });
            return Ok(portfolios);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserPortfolioEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatePortfolio(CreatePortfolioCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Created($"api/v1/portfolios/{command.Ticker}", null);
        }
        catch (SecurityAlreadyExistsException)
        {
            return Conflict();
        }
    }
}