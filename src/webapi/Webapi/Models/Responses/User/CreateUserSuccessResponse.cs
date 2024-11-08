namespace Webapi.Models.Responses.User;

public record CreateUserSuccessResponse
{
	public Guid Id {get; set;}
	public string Email {get; set;} = null!;
	public string UserName {get; set;} = null!;
}
