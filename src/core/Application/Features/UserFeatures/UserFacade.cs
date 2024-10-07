using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.Interface;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserProfile;

namespace Application.Features.UserFeatures;

public class UserFacade : IUserFacade
{
	private readonly ICreateUserHandler _createUserHandler;
	private readonly IDeleteUserHandler _deleteUserHandler;
	private readonly IGetAllUserHandler _getAllUserHandler;
	private readonly IGetByIdUserHandler _getByIdUserHandler;
	private readonly IUpdateUserHandler _updateUserHandler;
	private readonly IUpdateUserProfileHandler _updateUserProfileHandler;

	public UserFacade(
		ICreateUserHandler createUserHandler, 
		IDeleteUserHandler deleteUserHandler, 
		IGetAllUserHandler getAllUserHandler, 
		IGetByIdUserHandler getByIdUserHandler, 
		IUpdateUserHandler updateUserHandler, 
		IUpdateUserProfileHandler updateUserProfileHandler)
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

	public async Task<DeleteUserResponse?> DeleteUserAsync(DeleteUserRequest request)
	{
		return await _deleteUserHandler.HandleAsync(request);
	}

	public async Task<IEnumerable<GetAllUserResponse>?> GetAllUserAsync()
	{
		return await _getAllUserHandler.HandleAsync();
	}

	public async Task<GetByIdUserResponse?> GetByIdUserAsync(GetByIdUserRequest request)
	{
		return await _getByIdUserHandler.HandleAsync(request);
	}

	public async Task<UpdateUserResponse?> UpdateUserAsync(UpdateUserRequest request)
	{
		return await _updateUserHandler.HandleAsync(request);
	}

	public async Task<UpdateUserProfileResponse?> UpdateUserProfileAsync(UpdateUserProfileRequest request)
	{
		return await _updateUserProfileHandler.HandleAsync(request);
	}
}
