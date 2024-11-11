using Microsoft.AspNetCore.Identity;

namespace Webapi.Models.Responses.Role;

public record DeleteRoleFailedResponse
{
	public Guid Id {get; set;}
	public string RoleName {get; set;} = null!;
	public IEnumerable<IdentityError>? Errors {get; set;}
}
