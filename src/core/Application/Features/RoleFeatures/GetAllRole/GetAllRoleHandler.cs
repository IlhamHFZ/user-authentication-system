using Application.Features.RoleFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.RoleFeatures.GetAllRole;

public class GetAllRoleHandler : IGetAllRoleHandler
{
	private readonly RoleManager<Role> _roleManager;
	private readonly IMapper _mapper;
	private readonly ILogger<GetAllRoleHandler> _logger;

	public GetAllRoleHandler(
		RoleManager<Role> roleManager,
		IMapper mapper, 
		ILogger<GetAllRoleHandler> logger)
	{
		_roleManager = roleManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<IEnumerable<GetAllRoleResponse>> HandleAsync()
	{
		_logger.LogInformation("Starting to process GetAllRoleHandler request");
		var roles = await Task.Run(() => _roleManager.Roles);
		if(roles is null)
		{
			_logger.LogInformation("Roles not found");
			return Enumerable.Empty<GetAllRoleResponse>();			
		}
		
		_logger.LogInformation($"Successfully retrieved {roles.Count()} roles");
		return _mapper.Map<IEnumerable<GetAllRoleResponse>>(roles);
	}
}
