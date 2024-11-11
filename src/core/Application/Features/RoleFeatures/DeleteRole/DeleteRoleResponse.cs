using Microsoft.AspNetCore.Identity;

namespace Application.Features.RoleFeatures.DeleteRole;

public record DeleteRoleResponse
{
	public Guid Id {get; set;}
	public string RoleName {get; set;} = null!;
	public bool IsSuccess {get; set;}
	public IEnumerable<IdentityError>? Errors {get; set;}
}
