namespace Application.Features.UserFeatures.DeleteUser;

public record class DeleteUserResponse
{
	public string Email {get; set;}
	public string UserName {get; set;}
	public string DisplayName {get; set;}
}
