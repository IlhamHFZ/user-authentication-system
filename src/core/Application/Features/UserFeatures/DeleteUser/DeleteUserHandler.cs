using AutoMapper;
using Domain.Entites;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.DeleteUser;

public class DeleteUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	
	public DeleteUserHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	
	public async Task<DeleteUserResponse> Handler(DeleteUserRequest request)
	{
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		if(user is null)
		{
			return null;
		}
		
		_unitofWork.Repository<User>().Delete(user);
		await _unitofWork.SaveChangeAsync();
		
		return _mapper.Map<DeleteUserResponse>(user);
	}
}
