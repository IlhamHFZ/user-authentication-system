using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.UpdateUserProfile;

public class UpdateUserProfileHandler : IUpdateUserProfileHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<UpdateUserProfileRequest> _validator;

	public UpdateUserProfileHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		IValidator<UpdateUserProfileRequest> validator)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
	}

	public async Task<UpdateUserProfileResponse?> HandleAsync(UpdateUserProfileRequest request)
	{
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_validator.ValidateAndThrow(request);
		}
		
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		if(user is null)
		{
			return null;
		}
		
		user.UserName = request.UserName ?? user.UserName;
		user.DisplayName = request.DisplayName ?? user.DisplayName;
		
		await _unitofWork.SaveChangeAsync();
		
		return _mapper.Map<UpdateUserProfileResponse>(user);
	}
}
