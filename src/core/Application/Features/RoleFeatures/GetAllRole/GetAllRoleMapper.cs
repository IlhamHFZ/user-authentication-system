using AutoMapper;
using Domain.Entites;

namespace Application.Features.RoleFeatures.GetAllRole;

public class GetAllRoleMapper : Profile
{
	public GetAllRoleMapper()
	{
		CreateMap<Role, GetAllRoleResponse>();
	}
}
