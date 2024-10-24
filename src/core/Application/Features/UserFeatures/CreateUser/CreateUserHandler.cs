using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserHandler : ICreateUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<CreateUserRequest> _validator;
	private readonly ILogger<CreateUserHandler> _logger;

	public CreateUserHandler(
		IUnitofWork unitofWork,
		IMapper mapper,
		IValidator<CreateUserRequest> validator,
		ILogger<CreateUserHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
		_logger = logger;
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
		
		var user = _mapper.Map<User>(request);
		
		await _unitofWork.Repository<User>().CreateAsync(user);
		_logger.LogInformation($"Saving new user to database for user with email {request.Email}");
		
		await _unitofWork.SaveChangeAsync();
		_logger.LogInformation($"User successfully saved in database for user with email {request.Email}");
				
		return _mapper.Map<CreateUserResponse>(user);
	}
}
