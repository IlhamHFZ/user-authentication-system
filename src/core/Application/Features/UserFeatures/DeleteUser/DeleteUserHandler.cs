using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.DeleteUser;

public class DeleteUserHandler : IDeleteUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<DeleteUserRequest> _validator;
	private readonly ILogger<DeleteUserHandler> _logger;

	public DeleteUserHandler(
		IUnitofWork unitofWork,
		IMapper mapper,
		IValidator<DeleteUserRequest> validator,
		ILogger<DeleteUserHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
		_logger = logger;
	}

	public async Task<DeleteUserResponse?> HandleAsync(DeleteUserRequest request)
	{
		_logger.LogInformation($"Starting to process DeleteUserHandler request for user with id {request.Id}");
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_logger.LogWarning($"Validation failed for DeleteUserHandler request for user with id {request.Id}");
			_validator.ValidateAndThrow(request);
		}
		
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		if(user is null)
		{
			_logger.LogWarning($"User not found for user with id {request.Id}");
			return null;
		}
		
		_logger.LogInformation($"Delete user from database for user with id {request.Id}");
		_unitofWork.Repository<User>().Delete(user);
		
		await _unitofWork.SaveChangeAsync();
		_logger.LogInformation($"User successfully deleted from database for user with id {request.Id}");
		
		return _mapper.Map<DeleteUserResponse>(user);
	}
}
