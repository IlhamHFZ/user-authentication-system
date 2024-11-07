using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserFeatures.DeleteUser;

public class DeleteUserMapper : Profile
{
	public DeleteUserMapper()
	{
		CreateMap<User, DeleteUserResponse>();
		CreateMap<IdentityResult, DeleteUserResponse>()
			.ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Succeeded));
	}
}
