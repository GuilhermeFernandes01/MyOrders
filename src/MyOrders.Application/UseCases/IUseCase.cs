namespace MyOrders.Application.UseCases;

public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken);
}

public interface IUseCase<TResponse>
{
    Task<TResponse> Execute(CancellationToken cancellationToken);
}