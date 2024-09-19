using Application.Repository.Interface;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.GetByIdUser;

public class GetByIdUserHandler
{
	public readonly IRepository<User> _repository;
	public readonly IMapper _mapper;

	public GetByIdUserHandler(IRepository<User> repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
	
	public async Task<GetByIdUserResponse> Handle(GetByIdUserRequest request)
	{
		var user = await _repository.GetAsync(request.Id);
		return _mapper.Map<GetByIdUserResponse>(user);
	}
}
