namespace SitRep.Core.Domain;

public interface IQueryHandler
{
    
}

public interface IQueryHandler<out TResponse>
{
    TResponse Handle();
}
