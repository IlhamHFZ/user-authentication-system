using Application.Features.RoleFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.RoleFeatures.CreateRole;

public class CreateRoleHandler : ICreateRoleHandler
{
	private readonly RoleManager<Role> _roleManager;
	private readonly IMapper _mapper;
	private readonly ILogger<CreateRoleHandler> _logger;

	public CreateRoleHandler(
		RoleManager<Role> roleManager,
		IMapper mapper, 
		ILogger<CreateRoleHandler> logger)
	{
		_roleManager = roleManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreateRoleResponse> HandleAsync(CreateRoleRequest request)
	{
		_logger.LogInformation($"Starting to process CreateRoleHandler request for role with name {request.RoleName}");
		var newRole = _mapper.Map<Role>(request);
		
		_logger.LogInformation($"Create role for user with name {request.RoleName}");
		var identityResult = await _roleManager.CreateAsync(newRole);
		var response = _mapper.Map<CreateRoleResponse>(identityResult);
		response = _mapper.Map(newRole, response);
		if(!identityResult.Succeeded)
		{
			_logger.LogWarning($"Failed to create role for role with name {request.RoleName}");			
			return response;
		}
		
		
		_logger.LogInformation($"User successfully create in database for role with name {request.RoleName}");
		return response;
	}
}
