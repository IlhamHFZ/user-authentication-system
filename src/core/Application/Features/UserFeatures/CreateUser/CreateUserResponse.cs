using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserResponse
{
	public Guid? Id { get; set; }
	public string Email {get; set;} = null!;
	public string UserName {get; set;} = null!;
	public bool IsSuccess {get; set;}
	public IEnumerable<IdentityError>? Errors {get; set;}
}
