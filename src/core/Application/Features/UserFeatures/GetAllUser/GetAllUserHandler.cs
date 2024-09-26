using Application.Features.Interface;
using AutoMapper;
using Domain.Entites;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.GetAllUser;

public class GetAllUserHandler : IFeatureHandler<IEnumerable<GetAllUserResponse>, GetAllUserRequest>
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	
	public GetAllUserHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	
	public async Task<IEnumerable<GetAllUserResponse>> HandleAsync(GetAllUserRequest request)
	{
		var users = await _unitofWork.Repository<User>().GetAllAsync();
		return _mapper.Map<IEnumerable<GetAllUserResponse>>(users);
	}
}
