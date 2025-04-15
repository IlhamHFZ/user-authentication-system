using Application.Features.AuthFeatures.InternalRegister;

namespace Application.Features.AuthFeatures.Interface;

public interface IInternalRegisterHandler
{
    public Task<InternalRegisterResponse> HandleAsync(InternalRegisterRequest request);
}
