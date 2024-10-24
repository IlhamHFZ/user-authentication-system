using Application.Features.RoleFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.RoleFeatures.CreateRole;

public class CreateRoleHandler : ICreateRoleHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<CreateRoleRequest> _validator;
	private readonly ILogger<CreateRoleHandler> _logger;

	public CreateRoleHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		IValidator<CreateRoleRequest> validator, 
		ILogger<CreateRoleHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
		_logger = logger;
	}

	public async Task<CreateRoleResponse> HandleAsync(CreateRoleRequest request)
	{
		_logger.LogInformation($"Starting to process CreateRoleHandler request for role with name {request.Name}");
		var result = await _validator.ValidateAsync(request);
		if(!result.IsValid)
		{
			_logger.LogWarning($"Validation failed for CreateRoleHandler request for role with name {request.Name}");
			await _validator.ValidateAndThrowAsync(request);
		}
		
		var role = _mapper.Map<Role>(request);
		
		await _unitofWork.Repository<Role>().CreateAsync(role);
		_logger.LogInformation($"Saving new role to database for role with name {request.Name}");
		
		await _unitofWork.SaveChangeAsync();
		_logger.LogInformation($"Role successfullt saved in database for role with name {request.Name}");
		
		return _mapper.Map<CreateRoleResponse>(role);
	}
}
