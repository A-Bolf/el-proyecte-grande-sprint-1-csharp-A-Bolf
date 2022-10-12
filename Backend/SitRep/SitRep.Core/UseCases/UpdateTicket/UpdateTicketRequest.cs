using SitRep.Core.Domain;
using SitRep.Core.Entities;
using SitRep.Models;

namespace SitRep.Core.UseCases.UpdateTicket;

public class UpdateTicketRequest : IRequest<Response>
{


    public long Id { get; set; }
    public List<User> Assignees { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public StatusType Status { get; set; }
    public PriorityType Priority { get; set; }
    
}