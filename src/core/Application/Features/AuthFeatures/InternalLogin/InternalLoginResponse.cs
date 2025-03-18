namespace Application.Features.AuthFeatures.InternalLogin;

public record InternalLoginResponse
{
	public string? Token {get; set;}
	public bool IsSuccess {get; set;}
}
