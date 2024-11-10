using Application.Features.UserFeatures.UpdateUserAddRole;

namespace Application.Features.UserFeatures.Interface;

public interface IUpdateUserAddRoleHandler
{
	public Task<UpdateUserAddRoleResponse?> HandleAsync(UpdateUserAddRoleRequest request);
}
