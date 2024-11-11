using Microsoft.AspNetCore.Identity;

namespace Webapi.Models.Responses.Role;

public record CreateRoleFailedResponse
{
	public string RoleName {get; set;} = null!;
	public IEnumerable<IdentityError>? Errors {get; set;}
}
