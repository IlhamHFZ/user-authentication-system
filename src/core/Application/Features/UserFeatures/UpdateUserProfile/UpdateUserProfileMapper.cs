using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserFeatures.UpdateUserProfile;

public class UpdateUserProfileMapper : Profile
{
	public UpdateUserProfileMapper()
	{
		CreateMap<User, UpdateUserProfileResponse>();
		CreateMap<IdentityResult, UpdateUserProfileResponse>()
			.ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Succeeded));
	}
}
