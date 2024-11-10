namespace Application.Features.UserFeatures.UpdateUserAddRole;

public record UpdateUserAddRoleRequest
{
	public Guid UserId {get; set;}
	public Guid RoleId {get; set;}
}
