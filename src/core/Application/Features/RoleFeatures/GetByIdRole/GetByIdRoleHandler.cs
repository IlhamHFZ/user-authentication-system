using Application.Features.RoleFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.RoleFeatures.GetByIdRole;

public class GetByIdRoleHandler : IGetByIdRoleHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<GetByIdRoleRequest> _validator;
	private readonly ILogger<GetByIdRoleHandler> _logger;

	public GetByIdRoleHandler(
		IUnitofWork unitofWork,
		IMapper mapper,
		ILogger<GetByIdRoleHandler> logger,
		IValidator<GetByIdRoleRequest> validator)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_logger = logger;
		_validator = validator;
	}

	public async Task<GetByIdRoleResponse?> HandleAsync(GetByIdRoleRequest request)
	{
		_logger.LogInformation($"Starting to process GetByIdRoleHandler request for role with id {request.Id}");
		var result = await _validator.ValidateAsync(request);
		if(!result.IsValid)
		{
			_logger.LogWarning($"Validation failed for GetByIdRoleHandler request for role with id {request.Id}");
			await _validator.ValidateAndThrowAsync(request);
		}
		
		var role = await _unitofWork.Repository<Role>().GetAsync(request.Id);		
		if(role is null)
		{
			_logger.LogWarning($"Role not found for role with id {request.Id}");
			return null;
		}
		
		return _mapper.Map<GetByIdRoleResponse>(role);
	}
}
