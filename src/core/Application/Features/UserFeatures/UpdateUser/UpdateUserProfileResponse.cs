namespace Application.Features.UserFeatures.UpdateUser;

public record class UpdateUserProfileResponse
{
	public string UserName {get; set;}
	public string DisplayName {get; set;}
}
