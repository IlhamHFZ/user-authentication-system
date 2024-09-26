using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserProfile;

namespace Application.Features.UserFeatures;

public class UserFacade : IUserFacade
{
	private readonly CreateUserHandler _createUserHandler;
	private readonly DeleteUserHandler _deleteUserHandler;
	private readonly GetAllUserHandler _getAllUserHandler;
	private readonly GetByIdUserHandler _getByIdUserHandler;
	private readonly UpdateUserHandler _updateUserHandler;
	private readonly UpdateUserProfileHandler _updateUserProfileHandler;

	public UserFacade(
		CreateUserHandler createUserHandler, 
		DeleteUserHandler deleteUserHandler, 
		GetAllUserHandler getAllUserHandler, 
		GetByIdUserHandler getByIdUserHandler, 
		UpdateUserHandler updateUserHandler, 
		UpdateUserProfileHandler updateUserProfileHandler)
	{
		_createUserHandler = createUserHandler;
		_deleteUserHandler = deleteUserHandler;
		_getAllUserHandler = getAllUserHandler;
		_getByIdUserHandler = getByIdUserHandler;
		_updateUserHandler = updateUserHandler;
		_updateUserProfileHandler = updateUserProfileHandler;
	}

	public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
	{
		return await _createUserHandler.HandleAsync(request);
	}

	public async Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request)
	{
		return await _deleteUserHandler.HandleAsync(request);
	}

	public async Task<IEnumerable<GetAllUserResponse>> GetAllUserAsync(GetAllUserRequest request)
	{
		return await _getAllUserHandler.HandleAsync(request);
	}

	public async Task<GetByIdUserResponse> GetByIdUserAsync(GetByIdUserRequest request)
	{
		return await _getByIdUserHandler.HandleAsync(request);
	}

	public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request)
	{
		return await _updateUserHandler.HandleAsync(request);
	}

	public async Task<UpdateUserProfileResponse> UpdateUserProfileAsync(UpdateUserProfileRequest request)
	{
		return await _updateUserProfileHandler.HandleAsync(request);
	}
}
