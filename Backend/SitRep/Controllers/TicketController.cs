using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SitRep.Core;
using SitRep.Core.Entities;
using SitRep.Core.UseCases.DeleteTicket;
using SitRep.Core.UseCases.GetAllTickets;
using SitRep.DAL;
using SitRep.DAL;
using SitRep.Models;
using SitRep.SitRep.Core.DTOs;

namespace SitRep.Controllers;

[ApiController]
[Route("/api/ticket")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ISitRepContext _context;
    private readonly ILogger<TicketController> _logger;
    public TicketController(ITicketService ticketService, ISitRepContext sitRepContext, ILogger<TicketController> logger)
    {
        _logger = logger;
        _context = sitRepContext;
        _ticketService = ticketService;
    }

    [HttpGet("/api/ticket")]
    public ActionResult<List<Core.Entities.Ticket>> GetAll()
    {
        var ticketHandler = new GetAllTicketsHandler(_context);
        var ticketResponse = ticketHandler.Handle();
        if (ticketResponse.Failure)
        {
            _logger.LogError(ticketResponse.Error);
        }
        else
        {
            _logger.LogInformation("Success!");
        }
        return ticketResponse.Value;

    }

[HttpGet("/api/ticket/updates")]
public IActionResult GetRecentUpdates()
{
    return Ok(_ticketService.GetRecentUpdates());
}

[HttpGet("/api/ticket/statuscounts")]
public IActionResult GetStatusCounts()
{
    return Ok(_ticketService.GetStatusCounts());
}

[HttpGet("/api/ticket/{id}")]
public IActionResult GetById([FromRoute] int id)
{
    return Ok(_ticketService.GetById(id));
}

[HttpPost]
public ActionResult<TicketDTO> Create(TicketDTO ticketDTO)
{
    _ticketService.Add(new Ticket(ticketDTO));


    return Ok(_ticketService.GetAll());
}

[HttpPut("/api/ticket/update")]
public IActionResult Update([FromBody] Ticket ticket)
{
    _ticketService.Update(ticket);
    return Ok(ticket);
}

[HttpDelete("/api/ticket/delete/{id}")]
public IActionResult Delete([FromRoute] int id)
{
    DeleteTicketRequest request = new DeleteTicketRequest(id);
    DeleteTicketHandler handler = new DeleteTicketHandler(_context);
    var response = handler.Handle(request);
    if (response.Failure)
    {
        _logger.LogError(response.Error);
    }
    else
    {
        _logger.LogInformation("Delete successful!");
    }

    return Ok(response);
}


}