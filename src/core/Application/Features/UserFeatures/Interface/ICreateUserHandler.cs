using Application.Features.UserFeatures.CreateUser;

namespace Application.Features.UserFeatures.Interface;

public interface ICreateUserHandler
{
	Task<CreateUserResponse> HandleAsync(CreateUserRequest request);
}
