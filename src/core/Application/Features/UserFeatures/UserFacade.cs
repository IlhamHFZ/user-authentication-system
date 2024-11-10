using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.Interface;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserAddRole;
using Application.Features.UserFeatures.UpdateUserProfile;
using Application.Features.UserFeatures.UpdateUserRemoveRole;

namespace Application.Features.UserFeatures;

public class UserFacade : IUserFacade
{
	private readonly ICreateUserHandler _createUserHandler;
	private readonly IDeleteUserHandler _deleteUserHandler;
	private readonly IGetAllUserHandler _getAllUserHandler;
	private readonly IGetByIdUserHandler _getByIdUserHandler;
	private readonly IUpdateUserHandler _updateUserHandler;
	private readonly IUpdateUserAddRoleHandler _updateUserAddRoleHandler;
	private readonly IUpdateUserRemoveRoleHandler _updateUserRemoveHandler;
	private readonly IUpdateUserProfileHandler _updateUserProfileHandler;

	public UserFacade(
		ICreateUserHandler createUserHandler, 
		IDeleteUserHandler deleteUserHandler, 
		IGetAllUserHandler getAllUserHandler, 
		IGetByIdUserHandler getByIdUserHandler, 
		IUpdateUserHandler updateUserHandler, 
		IUpdateUserAddRoleHandler updateUserAddRoleHandler,
		IUpdateUserRemoveRoleHandler updateUserRemoveRoleHandler,
		IUpdateUserProfileHandler updateUserProfileHandler)
	{
		_createUserHandler = createUserHandler;
		_deleteUserHandler = deleteUserHandler;
		_getAllUserHandler = getAllUserHandler;
		_getByIdUserHandler = getByIdUserHandler;
		_updateUserHandler = updateUserHandler;
		_updateUserAddRoleHandler = updateUserAddRoleHandler;
		_updateUserRemoveHandler = updateUserRemoveRoleHandler;
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

	public async Task<IEnumerable<GetAllUserResponse>> GetAllUserAsync(GetAllUserRequest request)
	{
		return await _getAllUserHandler.HandleAsync(request);
	}

	public async Task<GetByIdUserResponse?> GetByIdUserAsync(GetByIdUserRequest request)
	{
		return await _getByIdUserHandler.HandleAsync(request);
	}

	public async Task<UpdateUserResponse?> UpdateUserAsync(UpdateUserRequest request)
	{
		return await _updateUserHandler.HandleAsync(request);
	}
	
	public async Task<UpdateUserAddRoleResponse?> UpdateUserAddRoleAsync(UpdateUserAddRoleRequest request)
	{
		return await _updateUserAddRoleHandler.HandleAsync(request);
	}
	
	public async Task<UpdateUserRemoveRoleResponse?> UpdateUserRemoveRoleAsync(UpdateUserRemoveRoleRequest request)
	{
		return await _updateUserRemoveHandler.HandleAsync(request);
	}

	public async Task<UpdateUserProfileResponse?> UpdateUserProfileAsync(UpdateUserProfileRequest request)
	{
		return await _updateUserProfileHandler.HandleAsync(request);
	}
}
