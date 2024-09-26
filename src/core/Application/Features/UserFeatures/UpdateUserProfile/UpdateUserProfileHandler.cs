using Application.Features.Interface;
using AutoMapper;
using Domain.Entites;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.UpdateUserProfile;

public class UpdateUserProfileHandler : IFeatureHandler<UpdateUserProfileResponse, UpdateUserProfileRequest>
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	
	public UpdateUserProfileHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	
	public async Task<UpdateUserProfileResponse> HandleAsync(UpdateUserProfileRequest request)
	{
		var validator = new UpdateUserProfileValidator();
		var result = validator.Validate(request);
		if(!result.IsValid)
		{
			return null;
		}
		
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		user.UserName = request.UserName ?? user.UserName;
		user.DisplayName = request.DisplayName ?? user.DisplayName;
		
		await _unitofWork.SaveChangeAsync();
		
		return _mapper.Map<UpdateUserProfileResponse>(user);
	}
}
