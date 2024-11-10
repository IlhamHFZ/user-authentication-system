using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserFeatures.UpdateUserRemoveRole;

public class UpdateUserRemoveRoleHandler : IUpdateUserRemoveRoleHandler
{
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<Role> _roleManager;
	private readonly IMapper _mapper;
	private readonly ILogger<UpdateUserRemoveRoleHandler> _logger;
	
	public UpdateUserRemoveRoleHandler(
		UserManager<User> userManager, 
		RoleManager<Role> roleManager, 
		IMapper mapper, 
		ILogger<UpdateUserRemoveRoleHandler> logger)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<UpdateUserRemoveRoleResponse?> HandleAsync(UpdateUserRemoveRoleRequest request)
	{
		_logger.LogInformation($"Starting to process UpdateUserRemoveRoleHandler request for user with id {request.UserId}");
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
		
		var identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
		var response = _mapper.Map<UpdateUserRemoveRoleResponse>(identityResult);
		response = _mapper.Map(user, response);
		if(!identityResult.Succeeded)
		{
			_logger.LogWarning($"Failed to add role to user for user with id {request.UserId}");
			return response;
		}
		
		_logger.LogInformation($"User successfully added role to user in database for user with id {request.UserId}");
		return response;
	}
}
