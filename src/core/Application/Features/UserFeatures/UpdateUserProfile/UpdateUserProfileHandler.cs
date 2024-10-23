using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.UpdateUserProfile;

public class UpdateUserProfileHandler : IUpdateUserProfileHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<UpdateUserProfileRequest> _validator;
	private readonly ILogger<UpdateUserProfileHandler> _logger;

	public UpdateUserProfileHandler(
		IUnitofWork unitofWork,
		IMapper mapper,
		IValidator<UpdateUserProfileRequest> validator,
		ILogger<UpdateUserProfileHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
		_logger = logger;
	}

	public async Task<UpdateUserProfileResponse?> HandleAsync(UpdateUserProfileRequest request)
	{
		_logger.LogInformation($"Starting to process UpdateUserProfileHandler request for user with id {request.Id}");
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_logger.LogWarning($"Validation failed for UpdateUserProfileHandler request for user with id {request.Id}");
			_validator.ValidateAndThrow(request);
		}
		
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		if(user is null)
		{
			_logger.LogWarning($"User not found for user with id {request.Id}");
			return null;
		}
		
		user.UserName = request.UserName ?? user.UserName;
		user.DisplayName = request.DisplayName ?? user.DisplayName;
		
		await _unitofWork.SaveChangeAsync();
		_logger.LogInformation($"User successfully updated in database for user with id {request.Id}");
		
		return _mapper.Map<UpdateUserProfileResponse>(user);
	}
}
