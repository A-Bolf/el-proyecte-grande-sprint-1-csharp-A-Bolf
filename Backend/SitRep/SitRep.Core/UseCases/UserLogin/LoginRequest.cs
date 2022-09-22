using SitRep.Core.Domain;

namespace SitRep.Core.UseCases.UserLogin;

public class LoginRequest:IRequest, IRequest<Response>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}