using Application.Features.UserFeatures.UpdateUserRemoveRole;

namespace Application.Features.UserFeatures.Interface;

public interface IUpdateUserRemoveRoleHandler
{
	public Task<UpdateUserRemoveRoleResponse?> HandleAsync(UpdateUserRemoveRoleRequest request);
}
