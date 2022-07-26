using SitRep.Core.Entities;
namespace SitRep.Infrastructure.Persistence;
public class SitRepSeed
{
    private readonly SitRepContext context;

    public SitRepSeed(SitRepContext context)
    {
        this.context = context;
    }

    public void Seed()
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        if (!context.Tickets.Any())
        {
            context.Tickets.AddRange(
                new Ticket
                {
                    Title = "First Test Ticket",
                    Description = "This is the First Test Ticket in the Seed()",
                    Priority = PriorityType.MEDIUM,
                    CreatedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now
                },
                new Ticket
                {
                    Title = "Second Test Ticket",
                    Description = "This is the Second Test Ticket in the Seed()",
                    Priority = PriorityType.LOW,
                    CreatedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now
                });
            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            Console.WriteLine("adding users");
            context.Users.AddRange(
                new User
                {
                    UserName = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass")
                });
            context.SaveChanges();
        }

       
    }
}
