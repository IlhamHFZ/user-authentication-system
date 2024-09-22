using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.UpdateUser;

public class UpdateUserMapper : Profile
{
	public UpdateUserMapper()
	{
		CreateMap<User, UpdateUserResponse>();
		CreateMap<Role, UpdateUserResponse>()
			.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
	}
}
