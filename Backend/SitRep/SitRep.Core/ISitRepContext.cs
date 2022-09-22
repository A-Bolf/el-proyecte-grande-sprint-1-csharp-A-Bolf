using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SitRep.Core.Entities;

namespace SitRep.Core;

public interface ISitRepContext
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    
    void SaveChanges();

    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
}