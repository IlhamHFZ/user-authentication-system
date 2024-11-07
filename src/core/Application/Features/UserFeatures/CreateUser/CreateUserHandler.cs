using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
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

		var identityResult =  await _userManager.CreateAsync(newUser, request.Password);
		var response = _mapper.Map<CreateUserResponse>(identityResult);
		if(!identityResult.Succeeded)
		{
			_logger.LogWarning($"User creation failed for user with email {request.Email}");			
			return _mapper.Map(newUser, response);
		}
		
		_logger.LogInformation($"Saving new user to database for user with email {request.Email}");
		_logger.LogInformation($"User successfully saved in database for user with email {request.Email}");
		return _mapper.Map(newUser, response);
	}
}
