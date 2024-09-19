namespace Application.Features.UserFeatures.CreateUser;

public record class CreateUserRequest
{
	public string Email {get; set;}
	public string UserName {get; set;}
	public string Password {get; set;}
}
