namespace Application.Features.AuthFeatures.InternalLogin;

public record InternalLoginRequest
{
	public string Email {get; set;} = null!;
	public string Password {get; set;} = null!;
}
