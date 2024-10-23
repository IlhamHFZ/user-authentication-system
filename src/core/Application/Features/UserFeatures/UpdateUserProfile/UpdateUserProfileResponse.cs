namespace Application.Features.UserFeatures.UpdateUserProfile;

public record class UpdateUserProfileResponse
{
	public Guid Id { get; set; }
	public string UserName {get; set;} = null!;
	public string DisplayName {get; set;} = null!;
}
