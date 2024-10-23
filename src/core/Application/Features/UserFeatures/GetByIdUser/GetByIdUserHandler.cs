using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.GetByIdUser;

public class GetByIdUserHandler : IGetByIdUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<GetByIdUserRequest> _validator;
	private readonly ILogger<GetByIdUserHandler> _logger;

	public GetByIdUserHandler(
		IUnitofWork unitofWork,
		IMapper mapper,
		IValidator<GetByIdUserRequest> validator,
		ILogger<GetByIdUserHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
		_logger = logger;
	}

	public async Task<GetByIdUserResponse?> HandleAsync(GetByIdUserRequest request)
	{
		_logger.LogInformation($"Starting to process GetByIdUserHandler request for user with id {request.Id}");
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_logger.LogWarning($"Validation failed for GetByIdUserHandler request for user with id {request.Id}");
			_validator.ValidateAndThrow(request);
		}
		
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		if(user is null)
		{
			_logger.LogWarning($"User not found for user with id {request.Id}");
			return null;
		}
		
		return _mapper.Map<GetByIdUserResponse>(user);
	}
}
