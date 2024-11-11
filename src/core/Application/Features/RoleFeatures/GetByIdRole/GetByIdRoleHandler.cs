using Application.Features.RoleFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.RoleFeatures.GetByIdRole;

public class GetByIdRoleHandler : IGetByIdRoleHandler
{
	private readonly RoleManager<Role> _roleManager;
	private readonly IMapper _mapper;
	private readonly ILogger<GetByIdRoleHandler> _logger;

	public GetByIdRoleHandler(
		RoleManager<Role> roleManager,
		IMapper mapper,
		ILogger<GetByIdRoleHandler> logger)
	{
		_roleManager = roleManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<GetByIdRoleResponse?> HandleAsync(GetByIdRoleRequest request)
	{
		_logger.LogInformation($"Starting to process GetByIdRoleHandler request for user with id {request.Id}");
		var role = await _roleManager.FindByIdAsync(request.Id.ToString());
		if(role is null)
		{
			_logger.LogWarning($"Role not found for role with id {request.Id}");
			return null;
		}
		
		_logger.LogInformation($"Successfully retrieved role with id {request.Id}");
		return _mapper.Map<GetByIdRoleResponse>(role);
	}
}
