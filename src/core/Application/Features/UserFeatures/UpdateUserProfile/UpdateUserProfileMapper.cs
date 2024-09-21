using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.UpdateUserProfile;

public class UpdateUserProfileMapper : Profile
{
	public UpdateUserProfileMapper()
	{
		CreateMap<UpdateUserProfileRequest, User>();
		CreateMap<User, UpdateUserProfileResponse>();
	}
}
