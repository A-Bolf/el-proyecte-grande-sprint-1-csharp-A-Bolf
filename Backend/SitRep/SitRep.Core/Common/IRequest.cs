namespace SitRep.Core.Domain;

public interface IRequest
{
    
}

public interface IRequest<out TResponse> {}