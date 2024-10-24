using AutoMapper;
using Domain.Entites;

namespace Application.Features.RoleFeatures.GetByIdRole;

public class GetByIdRoleMapper : Profile
{
	public GetByIdRoleMapper()
	{
		CreateMap<Role, GetByIdRoleResponse>();
	}
}
