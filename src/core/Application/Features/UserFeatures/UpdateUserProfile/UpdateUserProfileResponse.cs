namespace Application.Features.UserFeatures.UpdateUserProfile;

public record class UpdateUserProfileResponse
{
	public string UserName {get; set;}
	public string DisplayName {get; set;}
}
