using AutoMapper;
using Domain.Entites;

namespace Application.Features.RoleFeatures.DeleteRole;

public class DeleteRoleMapper : Profile
{
	public DeleteRoleMapper()
	{
		CreateMap<Role, DeleteRoleResponse>();
	}
}
