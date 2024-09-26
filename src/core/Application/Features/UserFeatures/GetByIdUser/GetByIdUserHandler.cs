using Application.Features.Interface;
using AutoMapper;
using Domain.Entites;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.GetByIdUser;

public class GetByIdUserHandler : IFeatureHandler<GetByIdUserResponse, GetByIdUserRequest>
{
	private readonly IUnitofWork _unitofWork;
	public readonly IMapper _mapper;

	public GetByIdUserHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	
	public async Task<GetByIdUserResponse> HandleAsync(GetByIdUserRequest request)
	{
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		return _mapper.Map<GetByIdUserResponse>(user);
	}
}
