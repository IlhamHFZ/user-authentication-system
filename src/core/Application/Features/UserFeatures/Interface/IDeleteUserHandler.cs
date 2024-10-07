using Application.Features.UserFeatures.DeleteUser;

namespace Application.Features.UserFeatures.Interface;

public interface IDeleteUserHandler
{
	Task<DeleteUserResponse?> HandleAsync(DeleteUserRequest request);
}
