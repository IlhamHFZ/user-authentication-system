using Application.Features.RoleFeatures.GetByIdRole;
using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserHandler : ICreateUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<CreateUserRequest> _validator;
	private readonly ILogger<CreateUserHandler> _logger;
	private readonly UserManager<User> _userManager;

	public CreateUserHandler(
		IUnitofWork unitofWork,
		IMapper mapper,
		IValidator<CreateUserRequest> validator,
		ILogger<CreateUserHandler> logger,
		UserManager<User> userManager)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
		_logger = logger;
		_userManager = userManager;
	}

	public async Task<CreateUserResponse> HandleAsync(CreateUserRequest request)
	{
		_logger.LogInformation($"Starting to process CreateUserHandler for user with email {request.Email}");
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_logger.LogWarning($"Validation failed for CreateUserHandler request for user with email {request.Email}");
			_validator.ValidateAndThrow(request);
		}

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
			return _mapper.Map(request, response);
		}
		
		_logger.LogInformation($"Saving new user to database for user with email {request.Email}");
		_logger.LogInformation($"User successfully saved in database for user with email {request.Email}");

		return _mapper.Map(request, response);
	}
}
