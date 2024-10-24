using AutoMapper;
using Domain.Entites;

namespace Application.Features.RoleFeatures.CreateRole;

public class CreateRoleMapper : Profile
{
	public CreateRoleMapper()
	{
		CreateMap<Role, CreateRoleResponse>().ReverseMap();
	}
}
