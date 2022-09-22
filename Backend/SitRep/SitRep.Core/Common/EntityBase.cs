namespace SitRep.Core.Domain;

public abstract class EntityBase
{
    public int Id { get; set; }
    public List<DomainEventBase> Events = new List<DomainEventBase>();
}

