namespace Application.Features.UserFeatures.CreateUser;

public record class CreateUserRequest
{
	public string Email {get; set;} = null!;
	public string UserName {get; set;} = null!;
	public string DisplayName {get; set;} = null!;
	public string Password {get; set;} = null!;
}
