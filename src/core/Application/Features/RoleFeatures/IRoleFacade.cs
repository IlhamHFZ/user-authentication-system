using Application.Features.RoleFeatures.CreateRole;
using Application.Features.RoleFeatures.DeleteRole;
using Application.Features.RoleFeatures.GetAllRole;
using Application.Features.RoleFeatures.GetByIdRole;

namespace Application.Features.RoleFeatures;

public interface IRoleFacade
{
	public Task<CreateRoleResponse> CreateRoleAsync(CreateRoleRequest request);
	public Task<DeleteRoleResponse?> DeleteRoleAsync(DeleteRoleRequest request);
	public Task<IEnumerable<GetAllRoleResponse>?> GetAllRoleAsync();
	public Task<GetByIdRoleResponse?> GetByIdRoleAsync(GetByIdRoleRequest request);
}
