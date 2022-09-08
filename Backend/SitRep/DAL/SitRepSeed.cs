using SitRep.Models;
using SitRep.Models.Types;

namespace SitRep.DAL;

using Microsoft.EntityFrameworkCore;

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
                },
                new Ticket
                {
                    Title = "Second Test Ticket",
                    Description = "This is the Second Test Ticket in the Seed()",
                    Priority = PriorityType.LOW,
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

        if (!context.Projects.Any())
        {
            context.Projects.AddRange(
                new Project
                {
                    Name = "Project1"
                },
                new Project
                {
                    Name = "Project1"
                });
            context.SaveChanges();
        }
    }
}
