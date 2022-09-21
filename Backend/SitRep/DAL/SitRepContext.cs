using Microsoft.EntityFrameworkCore;
using SitRep.Core;
using SitRep.Core.Entities;


namespace SitRep.DAL;

public class SitRepContext:DbContext, ISitRepContext
{
    public SitRepContext(DbContextOptions<SitRepContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>().ToTable("Ticket");
        modelBuilder.Entity<User>().ToTable("User");
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    public void SaveChanges()
    {
        base.SaveChanges();
    }
}