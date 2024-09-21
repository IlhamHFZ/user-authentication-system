namespace Application.Features.UserFeatures.UpdateUserProfile;

public record class UpdateUserProfileRequest
{
	public Guid Id {get; set;}
	public string UserName {get; set;}
	public string DisplayName {get; set;}
}
