using SitRep.Core.Domain;
using SitRep.Core.Entities;
using SitRep.Models;

namespace SitRep.Core.UseCases.UpdateTicket;

public class UpdateTicketRequest : IRequest<Response>
{
    public UpdateTicketRequest(Ticket ticket)
    {
        Ticket = ticket;
    }
    
    public Ticket Ticket { get; set; }
}