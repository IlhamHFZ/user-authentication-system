namespace Application.Features.UserFeatures.UpdateUser;

public record class UpdateUserRequest
{
	public Guid UserId {get; set;}
	public Guid RoleId {get; set;}
}
