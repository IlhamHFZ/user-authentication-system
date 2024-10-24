namespace Application.Features.RoleFeatures.CreateRole;

public record CreateRoleRequest
{
	public string Name {get; set;} = null!;
}
