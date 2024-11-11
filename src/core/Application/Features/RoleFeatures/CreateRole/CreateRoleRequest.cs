namespace Application.Features.RoleFeatures.CreateRole;

public record CreateRoleRequest
{
	public string RoleName {get; set;} = null!;
}
