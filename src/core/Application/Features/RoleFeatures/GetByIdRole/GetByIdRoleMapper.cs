using AutoMapper;
using Domain.Entites;

namespace Application.Features.RoleFeatures.GetByIdRole;

public class GetByIdRoleMapper : Profile
{
	public GetByIdRoleMapper()
	{
		CreateMap<Role, GetByIdRoleResponse>()
			.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.NormalizeRoleName, opt => opt.MapFrom(src => src.NormalizedName));
	}
}
