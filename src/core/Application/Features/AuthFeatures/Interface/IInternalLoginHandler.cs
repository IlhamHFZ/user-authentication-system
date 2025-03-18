using Application.Features.AuthFeatures.InternalLogin;

namespace Application.Features.AuthFeatures.Interface;

public interface IInternalLoginHandler
{
	public Task<InternalLoginResponse?> HandleAsync(InternalLoginRequest request);
}
