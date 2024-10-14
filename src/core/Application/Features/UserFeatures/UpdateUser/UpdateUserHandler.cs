using Application.Features.UserFeatures.Interface;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.UpdateUser;

/*
TODO:
	-implement logger
*/
public class UpdateUserHandler : IUpdateUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<UpdateUserRequest> _validator;

	public UpdateUserHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		IValidator<UpdateUserRequest> validator)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
	}
	
	//TODO: implement error handling 
	public async Task<UpdateUserResponse?> HandleAsync(UpdateUserRequest request)
	{
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_validator.ValidateAndThrow(request);
		}
		
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
