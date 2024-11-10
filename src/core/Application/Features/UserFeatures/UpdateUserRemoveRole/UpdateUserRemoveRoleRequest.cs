namespace Application.Features.UserFeatures.UpdateUserRemoveRole;

public record UpdateUserRemoveRoleRequest
{
	public Guid UserId {get; set;}
	public Guid RoleId {get; set;}
}
