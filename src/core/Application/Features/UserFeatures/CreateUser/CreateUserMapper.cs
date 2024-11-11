using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserMapper : Profile
{
	public CreateUserMapper()
	{
		CreateMap<CreateUserRequest, User>();
		CreateMap<User, CreateUserResponse>();
		CreateMap<IdentityResult, CreateUserResponse>()
			.ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Succeeded));
	}
}
