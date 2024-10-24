using Application.Features.RoleFeatures.DeleteRole;

namespace Application.Features.RoleFeatures.Interface;

public interface IDeleteRoleHandler
{
	public Task<DeleteRoleResponse?> HandleAsync(DeleteRoleRequest request);
}
