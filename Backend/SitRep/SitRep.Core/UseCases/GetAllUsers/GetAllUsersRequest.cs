using SitRep.Core.Domain;

namespace SitRep.Core.UseCases.GetAllUsers;

public class GetAllUsersRequest : IRequest<Response>
{
    public string Token { get; set; } = string.Empty;
}