using SitRep.Core.Domain;

namespace SitRep.Core.UseCases.UpdatePassword;

public class UpdatePasswordRequest:IRequest, IRequest<Response>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}