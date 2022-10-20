using SitRep.Core.Domain;

namespace SitRep.Core.Common;

public abstract class EntityBase
{
    public int Id { get; set; }
    public List<DomainEventBase> Events = new List<DomainEventBase>();
}

