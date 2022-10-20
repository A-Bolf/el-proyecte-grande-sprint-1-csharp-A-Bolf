using SitRep.Core.Domain;
using SitRep.Core.Entities;
using SitRep.Infrastructure.Persistence;

namespace SitRep.Core.UseCases.GetAllUsers;

public class GetAllUsersHandler : IQueryHandler<Response<List<User>>>
{
    private ISitRepContext _context;

    public GetAllUsersHandler(ISitRepContext context)
    {
        _context = context;
    }
    
    public Response<List<User>> Handle()
    {
        var users = _context.Users.ToList();
        if (!users.Any())
        {
            return Response.Fail<List<User>>("No Users in database");
        }

        return Response.Ok(users);
    }
}