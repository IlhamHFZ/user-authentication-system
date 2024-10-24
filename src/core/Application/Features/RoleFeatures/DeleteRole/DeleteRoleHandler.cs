using Application.Features.RoleFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.RoleFeatures.DeleteRole;

public class DeleteRoleHandler : IDeleteRoleHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<DeleteRoleRequest> _validator;
	private readonly ILogger<DeleteRoleHandler> _logger;

	public DeleteRoleHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		IValidator<DeleteRoleRequest> validator, 
		ILogger<DeleteRoleHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
		_logger = logger;
	}

	public async Task<DeleteRoleResponse?> HandleAsync(DeleteRoleRequest request)
	{
		_logger.LogInformation($"Starting to process DeleteRoleHandler request for role with id {request.Id}");
		var result = await _validator.ValidateAsync(request);
		if(!result.IsValid)
		{
			_logger.LogError($"Validation failed for DeleteHandler request for role with id {request.Id}");
			await _validator.ValidateAndThrowAsync(request);
		}

		var role = await _unitofWork.Repository<Role>().GetAsync(request.Id);
		if(role is null)
		{
			_logger.LogWarning($"Role not found for role with id {request.Id}");
			return null;
		}
		
		_unitofWork.Repository<Role>().Delete(role);
		_logger.LogInformation($"Delete Role from database for role with id {request.Id}");
		
		await _unitofWork.SaveChangeAsync();
		_logger.LogInformation($"Role successfully deleted from database for role with id {request.Id}");
		
		return _mapper.Map<DeleteRoleResponse>(role);
	}
}
