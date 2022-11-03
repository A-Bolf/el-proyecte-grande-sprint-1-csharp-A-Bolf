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
        if (!context.Users.Any())
        {
            Console.WriteLine("adding users");
            context.Users.AddRange(
                new User
                {
                    UserName = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass")
                },
                new User
                {
                    UserName = "superman", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass")
                },
                new User
                {
                    UserName = "randomGuy", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass")
                },
                new User
                {
                    UserName = "user2", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass")
                }
            );


            context.SaveChanges();
        }

        if (!context.Tickets.Any())
        {
            Ticket FirstTicket = new Ticket
            {
                Title = "First Test Ticket",
                Description = "This is the First Test Ticket in the Seed()",

                Priority = PriorityType.MEDIUM,
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                Assignees = new List<User>
                    { new User { UserName = "FirstUser", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass") } }
            };
            Ticket SecondTicket = new Ticket
            {
                Title = "Second Test Ticket",
                Description = "This is the Second Test Ticket in the Seed()",
                Priority = PriorityType.LOW,
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                Assignees = new List<User>
                    { new User { UserName = "SecondUser", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass") } }
            };
            Console.WriteLine("asd");
            context.Tickets.AddRange(FirstTicket, SecondTicket
            );
            context.SaveChanges();
        }
    }
}