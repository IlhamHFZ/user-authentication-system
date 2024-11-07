using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserHandler : ICreateUserHandler
{
	private readonly IMapper _mapper;
	private readonly ILogger<CreateUserHandler> _logger;
	private readonly UserManager<User> _userManager;

	public CreateUserHandler(
		IMapper mapper,
		ILogger<CreateUserHandler> logger,
		UserManager<User> userManager)
	{
		_mapper = mapper;
		_logger = logger;
		_userManager = userManager;
	}

	public async Task<CreateUserResponse> HandleAsync(CreateUserRequest request)
	{
		_logger.LogInformation($"Starting to process CreateUserHandler for user with email {request.Email}");
		var newUser = new User()
		{
			UserName = request.UserName,
			Email = request.Email,
			DisplayName = request.DisplayName
		};

		_logger.LogInformation($"Create user for user with email {request.Email}");
		var identityResult =  await _userManager.CreateAsync(newUser, request.Password);
		
		var response = _mapper.Map<CreateUserResponse>(identityResult);
		response = _mapper.Map(newUser, response);
		if(!identityResult.Succeeded)
		{
			_logger.LogWarning($"Failed to create user for user with email {request.Email}");			
			return response;
		}
		
		_logger.LogInformation($"User successfully create in database for user with email {request.Email}");
		return response;
	}
}
