using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserAddRole;
using Application.Features.UserFeatures.UpdateUserProfile;
using Application.Features.UserFeatures.UpdateUserRemoveRole;

namespace Application.Features.UserFeatures;

public interface IUserFacade
{
	Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
	Task<DeleteUserResponse?> DeleteUserAsync(DeleteUserRequest request);
	Task<IEnumerable<GetAllUserResponse>> GetAllUserAsync(GetAllUserRequest request);
	Task<GetByIdUserResponse?> GetByIdUserAsync(GetByIdUserRequest request);
	Task<UpdateUserResponse?> UpdateUserAsync(UpdateUserRequest request);
	Task<UpdateUserAddRoleResponse?> UpdateUserAddRoleAsync(UpdateUserAddRoleRequest request);
	Task<UpdateUserRemoveRoleResponse?> UpdateUserRemoveRoleAsync(UpdateUserRemoveRoleRequest request);
	Task<UpdateUserProfileResponse?> UpdateUserProfileAsync(UpdateUserProfileRequest request); 
}
