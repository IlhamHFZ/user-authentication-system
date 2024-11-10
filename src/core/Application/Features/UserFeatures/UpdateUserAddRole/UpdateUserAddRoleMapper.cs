using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserFeatures.UpdateUserAddRole;

public class UpdateUserAddRoleMapper : Profile
{
	public UpdateUserAddRoleMapper()
	{
		CreateMap<User, UpdateUserAddRoleResponse>();
		CreateMap<IdentityResult, UpdateUserAddRoleResponse>()
			.ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Succeeded));
	}
}
