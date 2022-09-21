using Microsoft.EntityFrameworkCore;
using SitRep.Core.Entities;

namespace SitRep.Core;

public interface ISitRepContext
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    
    void SaveChanges();
}