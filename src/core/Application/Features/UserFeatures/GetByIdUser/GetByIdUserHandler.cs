using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserFeatures.GetByIdUser;

public class GetByIdUserHandler : IGetByIdUserHandler
{
	private readonly UserManager<User> _userManager;
	private readonly IMapper _mapper;
	private readonly ILogger<GetByIdUserHandler> _logger;

	public GetByIdUserHandler(
		UserManager<User> userManager,
		IMapper mapper,
		ILogger<GetByIdUserHandler> logger)
	{
		_userManager = userManager;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<GetByIdUserResponse?> HandleAsync(GetByIdUserRequest request)
	{
		_logger.LogInformation($"Starting to process GetByIdUserHandler request for user with id {request.Id}");
		var user = await _userManager.FindByIdAsync(request.Id.ToString());
		if(user is null)
		{
			_logger.LogWarning($"User not found for user with id {request.Id}");
			return null;
		}
		
		_logger.LogInformation($"Successfully retrieved user with id {user.Id}");
		return _mapper.Map<GetByIdUserResponse>(user);
	}
}
