namespace Application.Features.RoleFeatures.CreateRole;

public record CreateRoleResponse
{
	public Guid Id {get; set;}
	public string RoleName {get; set;} = null!;
}
