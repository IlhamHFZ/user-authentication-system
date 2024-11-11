using Microsoft.AspNetCore.Identity;

namespace Application.Features.RoleFeatures.CreateRole;

public record CreateRoleResponse
{
	public Guid? Id {get; set;}
	public string RoleName {get; set;} = null!;
	public bool IsSuccess {get; set;}
	public IEnumerable<IdentityError>? Errors {get; set;}
}
