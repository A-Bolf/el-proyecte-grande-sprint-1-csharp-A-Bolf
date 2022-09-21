using SitRep.Core.Domain;

public interface IRequestHandler<in TRequest, out TResponse>
    where TRequest : IRequest<Response>
{
    
    TResponse Handle(TRequest message);
}


public interface IRequestHandler<in TRequest>
    where TRequest : IRequest
{
    void Handle(TRequest message);
}

