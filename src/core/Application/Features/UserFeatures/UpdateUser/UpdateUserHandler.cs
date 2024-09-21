using AutoMapper;
using Domain.Entites;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.UpdateUser;

public class UpdateUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	
	public UpdateUserHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	
	public async Task<UpdateUserResponse> Handle(UpdateUserRequest request)
	{
		var validator = new UpdateUserValidator();
		var result = validator.Validate(request);
		if(!result.IsValid)
		{
			return null;
		}
		
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		user.UserName = request.UserName ?? user.UserName;
		user.DisplayName = request.DisplayName ?? user.DisplayName;
		
		await _unitofWork.SaveChangeAsync();
		
		return _mapper.Map<UpdateUserResponse>(user);
	}
}
