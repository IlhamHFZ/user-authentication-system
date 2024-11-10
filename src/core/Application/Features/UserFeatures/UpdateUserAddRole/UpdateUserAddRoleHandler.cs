using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserFeatures.UpdateUserAddRole;

public class UpdateUserAddRoleHandler : IUpdateUserAddRoleHandler
{
	UserManager<User> _userManager;
	RoleManager<Role> _roleManager;
	IMapper _mapper;
	ILogger<UpdateUserAddRoleHandler> _logger;

	public UpdateUserAddRoleHandler(
		UserManager<User> userManager, 
		RoleManager<Role> roleManager, 
		IMapper mapper, 
		ILogger<UpdateUserAddRoleHandler> logger)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<UpdateUserAddRoleResponse?> HandleAsync(UpdateUserAddRoleRequest request)
	{
		_logger.LogInformation($"Starting to process UpdateUserAddRoleHandler request for user with id {request.UserId}");
		var user = await _userManager.FindByIdAsync(request.UserId.ToString());
		if(user is null)
		{
			_logger.LogWarning($"User not found for user with id {request.UserId}");
			return null;
		}
		
		var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
		if(role is null)
		{
			_logger.LogWarning($"Role not found for role with id {request.RoleId}");
			return null;
		}
		
		var identityResult = await _userManager.AddToRoleAsync(user, role.Name);
		var response = _mapper.Map<UpdateUserAddRoleResponse>(identityResult);
		response = _mapper.Map(user, response);
		response.RolesName = await _userManager.GetRolesAsync(user);
		if(!identityResult.Succeeded)
		{
			_logger.LogWarning($"Failed to add role to user for user with id {request.UserId}");
			return response;
		}
		
		_logger.LogInformation($"User successfully added role to user in database for user with id {request.UserId}");
		return response;
	}
}
