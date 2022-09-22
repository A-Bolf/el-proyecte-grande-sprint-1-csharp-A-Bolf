using SitRep.Core.Domain;

namespace SitRep.Core.UseCases.DeleteTicket;

public class DeleteTicketRequest:IRequest<Response>
{

    public DeleteTicketRequest(int id)
    {
        this.Id = id;
    }
    public int Id { get; set; }
}