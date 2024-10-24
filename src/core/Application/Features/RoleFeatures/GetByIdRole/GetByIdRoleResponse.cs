namespace Application.Features.RoleFeatures.GetByIdRole;

public record GetByIdRoleResponse
{
	public Guid Id {get; set;}
	public string RoleName {get; set;} = null!;
}
