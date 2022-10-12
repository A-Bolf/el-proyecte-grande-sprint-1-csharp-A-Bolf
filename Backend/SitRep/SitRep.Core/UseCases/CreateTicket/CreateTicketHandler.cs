using SitRep.Core.Entities;

namespace SitRep.Core.UseCases.CreateTicket;

public class CreateTicketHandler : IRequestHandler<CreateTicketRequest, Response<List<Ticket>>>
{
    private readonly ISitRepContext _context;

    public CreateTicketHandler(ISitRepContext context)
    {
        _context = context;
    }

    public Response<List<Ticket>> Handle(CreateTicketRequest message)
    {
        
        var tickets = _context.Tickets.ToList();
        if (message.Ticket.Title=="")
        {
            return Response.Fail<List<Ticket>>("FAil!");
        }
        if (message.Ticket.Description=="")
        {
            return Response.Fail<List<Ticket>>("FAil!");
        }
        message.Ticket.CreatedDate=DateTime.Today;
        message.Ticket.LastUpdatedDate=DateTime.Today;
        _context.Tickets.AddRange(message.Ticket);
        _context.SaveChanges();
        
        return Response.Ok(_context.Tickets.ToList());
    }
}