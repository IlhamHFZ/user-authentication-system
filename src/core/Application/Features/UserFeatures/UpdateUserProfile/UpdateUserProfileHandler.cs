using System.Diagnostics.CodeAnalysis;
using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.UpdateUserProfile;

public class UpdateUserProfileHandler : IUpdateUserProfileHandler
{
	private readonly UserManager<User> _userManager;
	private readonly IMapper _mapper;
	private readonly ILogger<UpdateUserProfileHandler> _logger;

	public UpdateUserProfileHandler(
		UserManager<User> userManager,
		IMapper mapper,
		ILogger<UpdateUserProfileHandler> logger)
	{
		_userManager = userManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<UpdateUserProfileResponse?> HandleAsync(UpdateUserProfileRequest request)
	{
		_logger.LogInformation($"Starting to process UpdateUserProfileHandler request for user with id {request.Id}");

		var user = await _userManager.FindByIdAsync(request.Id.ToString());
		if (user is null)
		{
			_logger.LogWarning($"User not found for user with id {request.Id}");
			return null;
		}

		user.UserName = request.UserName.Length != 0 ? request.UserName : user.UserName;
		user.DisplayName = request.DisplayName.Length != 0 ? request.DisplayName.ToUpperInvariant() : user.DisplayName;		
		
		var identityResult = await _userManager.UpdateAsync(user);
		var response = _mapper.Map<UpdateUserProfileResponse>(identityResult);
		response = _mapper.Map(user, response);
		if(!identityResult.Succeeded)
		{
			_logger.LogWarning($"Failed to update user profile for user with id {request.Id}");
			return response;
		}
		
		_logger.LogInformation($"User successfully updated in database for user with id {request.Id}");
		
		return response;
	}
}
