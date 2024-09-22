using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.UpdateUser;
/*
TODO:
	-implement logger
*/
public class UpdateUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	
	public UpdateUserHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	//TODO: implement error handling 
	public async Task<UpdateUserResponse> Handler(UpdateUserRequest request)
	{
		var user = await _unitofWork.Repository<IdentityUserRole<Guid>>().GetAsync(request.UserId);
		if(user is null)
		{
			return null;
		}
		
		var role = await _unitofWork.Repository<IdentityRole<Guid>>().GetAsync(request.RoleId);
		if(role is null)
		{
			return null;
		}
		
		user.RoleId = role.Id;
		
		await _unitofWork.SaveChangeAsync();
		
		var userDto = _mapper.Map<UpdateUserResponse>(user);
		return _mapper.Map(role, userDto);
	}
}
