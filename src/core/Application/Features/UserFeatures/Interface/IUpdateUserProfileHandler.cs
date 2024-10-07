using Application.Features.UserFeatures.UpdateUserProfile;

namespace Application.Features.UserFeatures.Interface;

public interface IUpdateUserProfileHandler
{
	Task<UpdateUserProfileResponse?> HandleAsync(UpdateUserProfileRequest request);
}
