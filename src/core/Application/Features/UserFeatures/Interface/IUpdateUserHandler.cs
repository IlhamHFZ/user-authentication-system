using Application.Features.UserFeatures.UpdateUser;

namespace Application.Features.UserFeatures.Interface;

public interface IUpdateUserHandler
{
	Task<UpdateUserResponse?> HandleAsync(UpdateUserRequest request);
}
