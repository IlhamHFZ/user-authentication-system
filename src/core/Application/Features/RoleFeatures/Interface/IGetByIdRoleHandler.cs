using Application.Features.RoleFeatures.GetByIdRole;

namespace Application.Features.RoleFeatures.Interface;

public interface IGetByIdRoleHandler
{
	public Task<GetByIdRoleResponse?> HandleAsync(GetByIdRoleRequest request);
}
