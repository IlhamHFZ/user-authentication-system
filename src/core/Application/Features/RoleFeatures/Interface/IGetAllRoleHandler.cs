using Application.Features.RoleFeatures.GetAllRole;

namespace Application.Features.RoleFeatures.Interface;

public interface IGetAllRoleHandler
{
	public Task<IEnumerable<GetAllRoleResponse>?> HandleAsync();
}
