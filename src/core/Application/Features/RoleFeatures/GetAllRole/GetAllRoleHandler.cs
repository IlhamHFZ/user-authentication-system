using System.Reflection.Metadata.Ecma335;
using Application.Features.RoleFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Cmp;
using Presistence.Repository.Interface;

namespace Application.Features.RoleFeatures.GetAllRole;

public class GetAllRoleHandler : IGetAllRoleHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly ILogger<GetAllRoleHandler> _logger;

	public GetAllRoleHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		ILogger<GetAllRoleHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<IEnumerable<GetAllRoleResponse>?> HandleAsync()
	{
		_logger.LogInformation("Starting to process GetAllRoleHandler request");
		var roles = await _unitofWork.Repository<Role>().GetAllAsync();
		if(roles is null)
		{
			_logger.LogInformation("Roles not found");
			return Enumerable.Empty<GetAllRoleResponse>();
		}
		
		return _mapper.Map<IEnumerable<GetAllRoleResponse>>(roles);
	}
}
