using SitRep.Core.Entities;
using SitRep.Infrastructure.Persistence;

namespace SitRep.Core.UseCases.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Response<User>>
{
    ISitRepContext _context;

    public RegisterUserHandler(ISitRepContext context)
    {
        _context = context;
    }

    public Response<User> Handle(RegisterUserRequest message)
    {
        try
        {
            if (_context.Users.Any(u => u.UserName == message.UserName))
            {
                return Response.Fail<User>("username taken");
            }

            var user = new User
            {
                UserName = message.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(message.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return Response.Ok<User>(user);
        }
        catch (Exception e)
        {
            return Response.Fail<User>(e.Message);
        }
    }
}