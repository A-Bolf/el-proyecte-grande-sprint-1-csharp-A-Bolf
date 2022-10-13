using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SitRep.Core.Entities;

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

    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Update(entity);
    }
}