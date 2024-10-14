using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.GetAllUser;

public class GetAllUserHandler : IGetAllUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	
	public GetAllUserHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	
	public async Task<IEnumerable<GetAllUserResponse>?> HandleAsync()
	{
		var users = await _unitofWork.Repository<User>().GetAllAsync();
		if(users is null)
		{
			return Enumerable.Empty<GetAllUserResponse>();
		}
		
		return _mapper.Map<IEnumerable<GetAllUserResponse>>(users);
	}
}
