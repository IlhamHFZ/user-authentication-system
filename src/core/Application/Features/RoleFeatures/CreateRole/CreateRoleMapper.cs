using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.RoleFeatures.CreateRole;

public class CreateRoleMapper : Profile
{
	public CreateRoleMapper()
	{
		CreateMap<CreateRoleRequest, Role>()
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));
		CreateMap<Role, CreateRoleResponse>()
			.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
		CreateMap<IdentityResult, CreateRoleResponse>()
			.ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Succeeded));
	}
}
