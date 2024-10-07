using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserProfile;

namespace Application.Features.UserFeatures;

public interface IUserFacade
{
	Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
	Task<DeleteUserResponse?> DeleteUserAsync(DeleteUserRequest request);
	Task<IEnumerable<GetAllUserResponse>?> GetAllUserAsync();
	Task<GetByIdUserResponse?> GetByIdUserAsync(GetByIdUserRequest request);
	Task<UpdateUserResponse?> UpdateUserAsync(UpdateUserRequest request);
	Task<UpdateUserProfileResponse?> UpdateUserProfileAsync(UpdateUserProfileRequest request); 
}
