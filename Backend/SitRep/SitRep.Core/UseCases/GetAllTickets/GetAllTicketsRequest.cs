using SitRep.Core.Domain;

namespace SitRep.Core.UseCases.GetAllTickets;

public class GetAllTicketsRequest : IRequest<Response>
{
    public string Token { get; set; } = string.Empty;
}