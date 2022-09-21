using SitRep.Core.Domain;
using SitRep.Core.Entities;

namespace SitRep.SitRep.Core.DTOs;

public class TicketResponse:Response
{
    public List<Ticket> Tickets = new List<Ticket>();
    public List<string> Errors = new List<string>();
    protected TicketResponse(bool success, List<string> errors, string message = null) : base(success, message)
    {
        Errors = errors;
    }
}