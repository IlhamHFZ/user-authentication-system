using Application.Features.UserFeatures.Interface;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.UpdateUser;

public class UpdateUserHandler : IUpdateUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<UpdateUserRequest> _validator;
	private readonly ILogger<UpdateUserHandler> _logger;

	public UpdateUserHandler(
		IUnitofWork unitofWork,
		IMapper mapper,
		IValidator<UpdateUserRequest> validator,
		ILogger<UpdateUserHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
		_logger = logger;
	}

	public async Task<UpdateUserResponse?> HandleAsync(UpdateUserRequest request)
	{
		_logger.LogInformation($"Starting to process UpdateUserHandler request for user with id {request.UserId}");
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_logger.LogWarning($"Validation failed for UpdateUserHandler request for user with id {request.UserId}");
			_validator.ValidateAndThrow(request);
		}
		
		var user = await _unitofWork.Repository<IdentityUserRole<Guid>>().GetAsync(request.UserId);
		if(user is null)
		{
			_logger.LogWarning($"User not found for user with id {request.UserId}");
			return null;
		}
		
		var role = await _unitofWork.Repository<IdentityRole<Guid>>().GetAsync(request.RoleId);
		if(role is null)
		{
			_logger.LogWarning($"Role not found for Role with id {request.RoleId}");
			return null;
		}
		
		user.RoleId = role.Id;
		
		await _unitofWork.SaveChangeAsync();
		_logger.LogInformation($"User successfully updated in database for user with id {request.UserId}");
		
		var userDto = _mapper.Map<UpdateUserResponse>(user);
		return _mapper.Map(role, userDto);
	}
}
