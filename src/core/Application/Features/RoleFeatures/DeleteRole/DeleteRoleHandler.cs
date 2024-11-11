using Application.Features.RoleFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.RoleFeatures.DeleteRole;

public class DeleteRoleHandler : IDeleteRoleHandler
{
	private readonly RoleManager<Role> _roleManager;
	private readonly IMapper _mapper;
	private readonly ILogger<DeleteRoleHandler> _logger;

	public DeleteRoleHandler(
		RoleManager<Role> roleManager,
		IMapper mapper, 
		ILogger<DeleteRoleHandler> logger)
	{
		_roleManager = roleManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<DeleteRoleResponse?> HandleAsync(DeleteRoleRequest request)
	{
		_logger.LogInformation($"Starting to process DeleteRoleHandler request for role with id {request.Id}");
		var role = await _roleManager.FindByIdAsync(request.Id.ToString());
		if(role is null)
		{
			_logger.LogWarning($"Role not found for role with id {request.Id}");
			return null;
		}
		
		_logger.LogInformation($"Delete Role from database for role with id {request.Id}");
		var identityResult = await _roleManager.DeleteAsync(role);
		var response = _mapper.Map<DeleteRoleResponse>(identityResult);
		response = _mapper.Map(role, response);
		if(!identityResult.Succeeded)
		{
			_logger.LogWarning($"Failed to delete role from database for role with id {request.Id}");
			return response;
		}
		
		_logger.LogInformation($"Role successfully deleted from database for role with id {request.Id}");
		return response;
	}
}
