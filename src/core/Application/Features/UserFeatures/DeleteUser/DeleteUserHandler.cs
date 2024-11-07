using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserFeatures.DeleteUser;

public class DeleteUserHandler : IDeleteUserHandler
{
	private readonly UserManager<User> _userManager;
	private readonly IMapper _mapper;
	private readonly ILogger<DeleteUserHandler> _logger;

	public DeleteUserHandler(
		UserManager<User> userManager,
		IMapper mapper,
		ILogger<DeleteUserHandler> logger)
	{
		_userManager = userManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<DeleteUserResponse?> HandleAsync(DeleteUserRequest request)
	{
		_logger.LogInformation($"Starting to process DeleteUserHandler request for user with id {request.Id}");
		
		User? user = await _userManager.FindByIdAsync(request.Id.ToString());
		if(user is null)
		{
			_logger.LogWarning($"User not found for user with id {request.Id}");
			return null;
		}
		
		_logger.LogInformation($"Delete user from database for user with id {request.Id}");
		IdentityResult identityResult = await _userManager.DeleteAsync(user);
		
		DeleteUserResponse response = _mapper.Map<DeleteUserResponse>(identityResult);
		response = _mapper.Map(user, response);
		if(!identityResult.Succeeded)
		{
			_logger.LogWarning($"Failed to delete user from database for user with id {request.Id}");
			return response;
		}
		
		_logger.LogInformation($"User successfully deleted from database for user with id {request.Id}");
		return response;
	}
}
