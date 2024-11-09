using Microsoft.AspNetCore.Identity;

namespace Webapi.Models.Responses.User;

public class UpdateUserProfileFailedResponse
{
	public Guid Id { get; set; }
	public string UserName {get; set;} = null!;
	public string DisplayName {get; set;} = null!;
	public IEnumerable<IdentityError> Errors {get; set;} = null!;
}
