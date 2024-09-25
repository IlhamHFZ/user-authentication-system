namespace Application.Features.Interface;

public interface IFeatureHandler<TResponse, TRequest> 
	where TResponse : class
	where TRequest : class
{
	public Task<TResponse> HandlerAsync(TRequest request);
}
