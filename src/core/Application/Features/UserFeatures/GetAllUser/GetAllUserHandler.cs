using Application.Repository.Interface;
using AutoMapper;
using Domain.Entites;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.GetAllUser;

public class GetAllUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	
	public GetAllUserHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	
	public async Task<IEnumerable<GetAllUserResponse>> Handle()
	{
		var users = await _unitofWork.Repository<User>().GetAllAsync();
		return _mapper.Map<IEnumerable<GetAllUserResponse>>(users);
	}
}
