namespace SitRep.Core.Domain;

public abstract class DomainEventBase
{
    public DateTime DateOccured { get; protected set; } = DateTime.UtcNow;
}