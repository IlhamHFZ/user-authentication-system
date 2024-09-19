using Application.Repository.Interface;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.GetAllUser;

public class GetAllUserHandler
{
	private readonly IRepository<User> _repository;
	private readonly IMapper _mapper;
	
	public GetAllUserHandler(IRepository<User> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	
	public async Task<IEnumerable<GetAllUserResponse>> Handle()
	{
		var users = await _repository.GetAllAsync();
		return _mapper.Map<IEnumerable<GetAllUserResponse>>(users);
	}
}
