using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.RoleFeatures.DeleteRole;

public class DeleteRoleMapper : Profile
{
	public DeleteRoleMapper()
	{
		CreateMap<Role, DeleteRoleResponse>()
			.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
		CreateMap<IdentityResult, DeleteRoleResponse>()
			.ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Succeeded));
	}
}
