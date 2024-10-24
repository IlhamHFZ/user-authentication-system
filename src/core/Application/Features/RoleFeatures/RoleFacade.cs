using Application.Features.RoleFeatures.CreateRole;
using Application.Features.RoleFeatures.DeleteRole;
using Application.Features.RoleFeatures.GetAllRole;
using Application.Features.RoleFeatures.GetByIdRole;
using Application.Features.RoleFeatures.Interface;

namespace Application.Features.RoleFeatures;

public class RoleFacade : IRoleFacade
{
	private readonly ICreateRoleHandler _createRoleHandler;
	private readonly IDeleteRoleHandler _deleteRoleHandler;
	private readonly IGetAllRoleHandler _getAllRoleHandler;
	private readonly IGetByIdRoleHandler _getByIdRoleHandler;

	public RoleFacade(
		ICreateRoleHandler createRoleHandler, 
		IDeleteRoleHandler deleteRoleHandler, 
		IGetAllRoleHandler getAllRoleHandler, 
		IGetByIdRoleHandler getByIdRoleHandler)
	{
		_createRoleHandler = createRoleHandler;
		_deleteRoleHandler = deleteRoleHandler;
		_getAllRoleHandler = getAllRoleHandler;
		_getByIdRoleHandler = getByIdRoleHandler;
	}

	public async Task<CreateRoleResponse> CreateRoleAsync(CreateRoleRequest request)
	{
		return  await _createRoleHandler.HandleAsync(request);
	}

	public async Task<DeleteRoleResponse?> DeleteRoleAsync(DeleteRoleRequest request)
	{
		return await _deleteRoleHandler.HandleAsync(request);
	}

	public async Task<IEnumerable<GetAllRoleResponse>?> GetAllRoleAsync()
	{
		return await _getAllRoleHandler.HandleAsync();
	}

	public async Task<GetByIdRoleResponse?> GetByIdRoleAsync(GetByIdRoleRequest request)
	{
		return await _getByIdRoleHandler.HandleAsync(request);
	}
}
