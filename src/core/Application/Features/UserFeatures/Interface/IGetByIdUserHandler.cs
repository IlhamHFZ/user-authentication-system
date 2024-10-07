using Application.Features.UserFeatures.GetByIdUser;

namespace Application.Features.UserFeatures.Interface;

public interface IGetByIdUserHandler
{
	Task<GetByIdUserResponse?> HandleAsync(GetByIdUserRequest request);
}
