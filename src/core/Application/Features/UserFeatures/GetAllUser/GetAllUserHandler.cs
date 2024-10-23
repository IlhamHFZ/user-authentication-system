using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.Extensions.Logging;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.GetAllUser;

public class GetAllUserHandler : IGetAllUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly ILogger<GetAllUserHandler> _logger;

	public GetAllUserHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		ILogger<GetAllUserHandler> logger)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<IEnumerable<GetAllUserResponse>?> HandleAsync()
	{
		_logger.LogInformation($"Starting to process GetAllUserHandler request");
		var users = await _unitofWork.Repository<User>().GetAllAsync();
		if(users is null)
		{
			_logger.LogWarning($"Users not found");
			return Enumerable.Empty<GetAllUserResponse>();
		}
		
		return _mapper.Map<IEnumerable<GetAllUserResponse>>(users);
	}
}
