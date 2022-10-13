using SitRep.Core.Domain;
using SitRep.Core.Entities;

namespace SitRep.Core.UseCases.CreateTicket;

public class CreateTicketRequest : IRequest<Response>
{
    public CreateTicketRequest(TicketDTO ticket)
    {
        Ticket = new Ticket(ticket);
    }

    public Ticket Ticket { get; set; }
}