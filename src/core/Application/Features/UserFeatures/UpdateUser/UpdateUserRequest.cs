namespace Application.Features.UserFeatures.UpdateUser;

public record class UpdateUserRequest
{
	public string UserName {get; set;}
	public string DisplayName {get; set;}
}
