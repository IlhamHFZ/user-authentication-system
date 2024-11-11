using AutoMapper;
using Domain.Entites;

namespace Application.Features.RoleFeatures.GetAllRole;

public class GetAllRoleMapper : Profile
{
	public GetAllRoleMapper()
	{
		CreateMap<Role, GetAllRoleResponse>()
			.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.NormalizeRoleName, opt => opt.MapFrom(src => src.NormalizedName));
	}
}
