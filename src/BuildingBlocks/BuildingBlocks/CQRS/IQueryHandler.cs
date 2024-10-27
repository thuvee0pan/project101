using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQueryHandler<in TQuert, TResponse> : IRequestHandler<TQuert, TResponse>
    where TQuert : IQuery<TResponse>
    where TResponse : notnull
{
    
}