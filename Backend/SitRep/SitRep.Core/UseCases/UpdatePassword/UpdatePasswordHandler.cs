namespace SitRep.Core.UseCases.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest,Response>
{
    private readonly ISitRepContext _context;

    public UpdatePasswordHandler(ISitRepContext context)
    {
        _context = context;
    }

    public Response Handle(UpdatePasswordRequest message)
    {
        var username = message.Username;
        var password = message.Password;
        var user = _context.Users.ToList().FirstOrDefault(u=>u.UserName==username,null);
        if (user == null)
        {
            return  Response.Fail($"User {username} does not exist");
        }
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        _context.Update(user);
        _context.SaveChanges();
        return Response.Ok();
    }
}