using SitRep.Infrastructure.Persistence;

namespace SitRep.Core.UseCases.DeleteTicket;

public class DeleteTicketHandler : IRequestHandler<DeleteTicketRequest,Response>
{
    private ISitRepContext _context;

    public DeleteTicketHandler(ISitRepContext context)
    {
        _context = context;
    }
    
    
    public Response Handle(DeleteTicketRequest message)
    {
        var Id = message.Id;
        var ticketToDelete = _context.Tickets.ToList().FirstOrDefault(t=>t.Id==Id,null);
        if (ticketToDelete==null)
        {
            return Response.Fail("Could Not Find Ticket");
        }

        _context.Tickets.Remove(ticketToDelete);
        _context.SaveChanges();
        return Response.Ok();

    }
}