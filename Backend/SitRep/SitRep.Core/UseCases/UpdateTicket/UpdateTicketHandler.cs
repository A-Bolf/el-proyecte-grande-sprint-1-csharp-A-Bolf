using Microsoft.EntityFrameworkCore;
using SitRep.Core.Entities;
using SitRep.Core.UseCases.CreateTicket;

namespace SitRep.Core.UseCases.UpdateTicket;

public class UpdateTicketHandler : IRequestHandler<UpdateTicketRequest, Response<Ticket>>
{
    private readonly ISitRepContext _context;

    public UpdateTicketHandler(ISitRepContext context)
    {
        _context = context;
    }
    
    public Response<Ticket> Handle(UpdateTicketRequest message)
    {
        var ticketToUpdate = _context.Tickets.ToList().FirstOrDefault(ticket => ticket.Id == message.Id,null);
        
         if (ticketToUpdate==null)
         {
             return Response.Fail<Ticket>("The ticket you are trying to update cannot be found in the database!");
         }
         ticketToUpdate.Assignees = message.Assignees;
        ticketToUpdate.Description = message.Description;
        ticketToUpdate.Priority = message.Priority;
        ticketToUpdate.Status = message.Status;
        ticketToUpdate.Title = message.Title;
        ticketToUpdate.LastUpdatedDate = DateTime.Now;
        _context.Update(ticketToUpdate);
        _context.SaveChanges();
        return Response.Ok(ticketToUpdate);

    }
}