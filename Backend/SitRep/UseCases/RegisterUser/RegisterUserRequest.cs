using SitRep.Core.Domain;

namespace SitRep.Core.UseCases.RegisterUser;

public class RegisterUserRequest : IRequest<Response>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}