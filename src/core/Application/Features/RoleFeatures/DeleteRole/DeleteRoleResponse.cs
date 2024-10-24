namespace Application.Features.RoleFeatures.DeleteRole;

public record DeleteRoleResponse
{
	public Guid Id {get; set;}
	public string RoleName {get; set;} = null!;
}
