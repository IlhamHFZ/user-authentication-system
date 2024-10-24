using Application.Features.RoleFeatures.CreateRole;

namespace Application.Features.RoleFeatures.Interface;

public interface ICreateRoleHandler
{
	public Task<CreateRoleResponse> HandleAsync(CreateRoleRequest request);
}
