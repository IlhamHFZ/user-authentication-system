namespace Application.Features.RoleFeatures.GetAllRole;

public record GetAllRoleResponse
{
	public Guid Id {get; set;}
	public string RoleName {get; set;} = null!;
}
