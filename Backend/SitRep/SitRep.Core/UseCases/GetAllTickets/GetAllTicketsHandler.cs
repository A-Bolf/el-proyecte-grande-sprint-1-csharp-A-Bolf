using SitRep.Core.Domain;
using SitRep.Core.Entities;

namespace SitRep.Core.UseCases.GetAllTickets;

public class GetAllTicketsHandler : IQueryHandler<Response<List<Ticket>>>
{
    private ISitRepContext _context;
    public GetAllTicketsHandler(ISitRepContext _context)
    {
        this._context = _context;
    }


    public Response<List<Ticket>> Handle()
    {
        var tickets =  _context.Tickets.ToList();
        if (!tickets.Any())
        {
            return Response.Fail<List<Ticket>>("No Tickets In DB");
        }

        return Response.Ok(tickets);
    }
}