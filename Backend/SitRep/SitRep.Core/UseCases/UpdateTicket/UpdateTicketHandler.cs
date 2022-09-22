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
        var ticketToUpdate = _context.Tickets.ToList().FirstOrDefault(ticket => ticket.Id == message.Ticket.Id,null);
        
         if (ticketToUpdate==null)
         {
             return Response.Fail<Ticket>("The ticket you are trying to update cannot be found in the database!");
         }
         ticketToUpdate.Assignees = message.Ticket.Assignees;
        ticketToUpdate.Description = message.Ticket.Description;
        ticketToUpdate.Priority = message.Ticket.Priority;
        ticketToUpdate.Status = message.Ticket.Status;
        ticketToUpdate.Title = message.Ticket.Title;
        ticketToUpdate.Type = message.Ticket.Type;
        ticketToUpdate.CreatedDate = message.Ticket.CreatedDate;
        ticketToUpdate.DueDate = message.Ticket.DueDate;
        ticketToUpdate.CreatorID = message.Ticket.CreatorID;
        ticketToUpdate.LastUpdatedDate = message.Ticket.LastUpdatedDate;
        _context.Update(ticketToUpdate);
        _context.SaveChanges();
        return Response.Ok(message.Ticket);

    }
}